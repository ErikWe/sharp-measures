namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Threading;

internal static class MarkedTypeDeclarationCandidateProvider
{
    public delegate TOut DOutputTransform<out TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax);

    public static IProviderBuilder<TOut> Construct<TOut>(DOutputTransform<TOut> outputTransform)
    {
        return new ProviderBuilder<TOut>(outputTransform);
    }

    public static IProviderBuilder<TypeDeclarationSyntax> Construct()
    {
        return new ProviderBuilder<TypeDeclarationSyntax>(extractDeclaration);

        static TypeDeclarationSyntax extractDeclaration(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax) => declaration;
    }

    public interface IProviderBuilder<TOut>
    {
        public abstract IncrementalValuesProvider<TOut> Attach<TAttribute>(SyntaxValueProvider syntaxProvider);
        public abstract IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, Type attributeType);
        public abstract IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, string attributeName);
    }

    private sealed class ProviderBuilder<TOut> : IProviderBuilder<TOut>
    {
        private DOutputTransform<TOut> OutputTransform { get; }

        public ProviderBuilder(DOutputTransform<TOut> outputTransform)
        {
            OutputTransform = outputTransform;
        }

        public IncrementalValuesProvider<TOut> Attach<TAttribute>(SyntaxValueProvider syntaxProvider)
        {
            return Attach(syntaxProvider, typeof(TAttribute));
        }

        public IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, Type attributeType)
        {
            return Attach(syntaxProvider, attributeType.FullName);
        }

        public IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, string attributeName)
        {
            Provider outputProvider = new(OutputTransform, attributeName);

            return Attach(syntaxProvider, outputProvider);
        }

        private static IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, AProvider outputProvider)
        {
            return outputProvider.Attach(syntaxProvider);
        }

        private abstract class AProvider
        {
            public DOutputTransform<TOut> OutputTransform { get; }

            public AProvider(DOutputTransform<TOut> outputTransform)
            {
                OutputTransform = outputTransform;
            }

            protected abstract AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration);

            public IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider)
            {
                return syntaxProvider.CreateSyntaxProvider(
                    predicate: SyntaxNodeIsTypeDeclarationWithAttributes,
                    transform: CandidateTypeDeclarationElseNull
                ).WhereNotNull().Select(ApplyOutputTransform);
            }

            private OutputData? CandidateTypeDeclarationElseNull(GeneratorSyntaxContext context, CancellationToken _)
            {
                TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

                return GetAttributeSyntax(context, declaration) is AttributeSyntax attributeSyntax ? new OutputData(declaration, attributeSyntax) : null;
            }

            private TOut ApplyOutputTransform(OutputData result, CancellationToken _)
            {
                return OutputTransform(result.Declaration, result.AttributeSyntax);
            }

            private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node, CancellationToken _)
            {
                return node is TypeDeclarationSyntax declaration && !declaration.Identifier.IsMissing && declaration.AttributeLists.Count > 0;
            }

            private readonly record struct OutputData(TypeDeclarationSyntax Declaration, AttributeSyntax AttributeSyntax);
        }

        private class Provider : AProvider
        {
            private string AttributeName { get; }

            public Provider(DOutputTransform<TOut> outputTransform, string attributeName) : base(outputTransform)
            {
                AttributeName = attributeName;
            }

            protected override AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration)
            {
                return declaration.GetAttributeWithName(AttributeName, context.SemanticModel);
            }
        }
    }
}
