namespace ErikWe.SharpMeasures.SourceGenerators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;

internal static class MarkedDeclarationSyntaxProvider
{
    public delegate TOut DOutputTransform<TOut>(TypeDeclarationSyntax typeDeclaration, AttributeSyntax attribute);

    public static IncrementalValuesProvider<TOut> Attach<TAttribute, TOut>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform)
        where TOut : struct
        => Attach(syntaxProvider, outputTransform, new Type[] { typeof(TAttribute) });

    public static IncrementalValuesProvider<TOut> Attach<TOut>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform,
        IEnumerable<Type> attributeTypes) where TOut : struct
    {
        return Attach(syntaxProvider, outputTransform, typesToStrings());

        IEnumerable<string> typesToStrings()
        {
            foreach (Type type in attributeTypes)
            {
                yield return type.FullName;
            }
        }
    }

    public static IncrementalValuesProvider<TOut> Attach<TOut>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform,
        IEnumerable<string> attributeNames) where TOut: struct
    {
        return syntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => IsSyntaxNodePartialTypeDeclarationWithAttributes(node),
            transform: (context, _) => MarkedTypeDeclarationElseNull<TOut>(context, outputTransform, attributeNames)
        ).WhereNotNull();
    }

    private static bool IsSyntaxNodePartialTypeDeclarationWithAttributes(SyntaxNode node)
    {
        static bool hasAttributes(TypeDeclarationSyntax declaration) => declaration.AttributeLists.Count > 0;

        static bool isPartial(TypeDeclarationSyntax declaration)
        {
            static bool isPartialModifier(SyntaxToken token) => token.IsKind(SyntaxKind.PartialKeyword);

            foreach (SyntaxToken token in declaration.Modifiers)
            {
                if (isPartialModifier(token))
                {
                    return true;
                }
            }

            return false;
        }

        return node is TypeDeclarationSyntax declaration && hasAttributes(declaration) && isPartial(declaration);
    }

    private static TOut? MarkedTypeDeclarationElseNull<TOut>(GeneratorSyntaxContext context, DOutputTransform<TOut> outputTransform, IEnumerable<string> attributeNames)
        where TOut : struct
    {
        ISymbol? getAttributeSymbol(AttributeSyntax syntax) => context.SemanticModel.GetSymbolInfo(syntax).Symbol; 
        bool isValidAttribute(INamedTypeSymbol attributeSymbol)
        {
            string candidateName = attributeSymbol.ToDisplayString();

            foreach (string attributeName in attributeNames)
            {
                if (candidateName == attributeName)
                {
                    return true;
                }    
            }

            return false;
        }

        TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

        foreach (AttributeListSyntax attributeList in declaration.AttributeLists)
        {
            foreach (AttributeSyntax attribute in attributeList.Attributes)
            {
                if (getAttributeSymbol(attribute) is IMethodSymbol attributeConstructorSymbol
                    && isValidAttribute(attributeConstructorSymbol.ContainingType))
                {
                    return outputTransform(declaration, attribute);
                }
            }
        }

        return null;
    }
}
