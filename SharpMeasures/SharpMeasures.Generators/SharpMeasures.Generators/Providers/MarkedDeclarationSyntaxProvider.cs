namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;

internal static class MarkedDeclarationSyntaxProvider
{
    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach<TAttribute>(SyntaxValueProvider syntaxProvider)
        => Attach(syntaxProvider, typeof(TAttribute));

    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach(SyntaxValueProvider syntaxProvider, Type attributeType)
        => Attach(syntaxProvider, attributeType.FullName);

    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach(SyntaxValueProvider syntaxProvider, string attributeName)
    {
        return syntaxProvider.CreateSyntaxProvider(
            predicate: static (node, _) => SyntaxNodeIsTypeDeclarationWithAttributes(node),
            transform: (context, _) => MarkedTypeDeclarationElseNull(context, attributeName)
        ).WhereNotNull();
    }

    private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node)
        => node is TypeDeclarationSyntax declaration && !declaration.Identifier.IsMissing && declaration.AttributeLists.Count > 0;

    private static TypeDeclarationSyntax? MarkedTypeDeclarationElseNull(GeneratorSyntaxContext context, string attributeName)
    {
        TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

        return declaration.HasAttributeWithName(context.SemanticModel, attributeName) ? declaration : null;
    }
}
