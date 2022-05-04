namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Threading;

internal static class MarkedTypeDeclarationCandidateProvider
{
    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach<TAttribute>(SyntaxValueProvider syntaxProvider)
        => Attach(syntaxProvider, typeof(TAttribute));

    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach(SyntaxValueProvider syntaxProvider, Type attributeType)
        => Attach(syntaxProvider, attributeType.FullName);

    public static IncrementalValuesProvider<TypeDeclarationSyntax> Attach(SyntaxValueProvider syntaxProvider, string attributeName)
    {
        Provider provider = new(attributeName);

        return provider.Attach(syntaxProvider);
    }

    private class Provider
    {
        private string AttributeName { get; }

        public Provider(string attributeName)
        {
            AttributeName = attributeName;
        }

        public IncrementalValuesProvider<TypeDeclarationSyntax> Attach(SyntaxValueProvider syntaxProvider)
        {
            return syntaxProvider.CreateSyntaxProvider(
                predicate: SyntaxNodeIsTypeDeclarationWithAttributes,
                transform: CandidateTypeDeclarationElseNull
            ).WhereNotNull();
        }

        private TypeDeclarationSyntax? CandidateTypeDeclarationElseNull(GeneratorSyntaxContext context, CancellationToken _)
        {
            TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

            return declaration.HasAttributeWithName(context.SemanticModel, AttributeName) ? declaration : null;
        }

        private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node, CancellationToken _)
        {
            return node is TypeDeclarationSyntax declaration && !declaration.Identifier.IsMissing && declaration.AttributeLists.Count > 0;
        }
    }
}
