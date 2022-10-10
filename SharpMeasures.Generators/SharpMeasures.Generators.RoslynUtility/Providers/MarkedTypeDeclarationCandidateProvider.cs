namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Threading;

public interface IMarkedTypeDeclarationCandidateProvider<TOut>
{
    public abstract IncrementalValuesProvider<Optional<TOut>> Attach<TAttribute>(SyntaxValueProvider syntaxProvider);
    public abstract IncrementalValuesProvider<Optional<TOut>> Attach(SyntaxValueProvider syntaxProvider, Type attributeType);
    public abstract IncrementalValuesProvider<Optional<TOut>> Attach(SyntaxValueProvider syntaxProvider, string attributeName);

    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf<TAttribute1, TAttribute2>(SyntaxValueProvider syntaxProvider);
    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf<TAttribute1, TAttribute2, TAttribute3>(SyntaxValueProvider syntaxProvider);

    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<Type> candidateTypes);
    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params Type[] candidateTypes);

    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<string> candidateNames);
    public abstract IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params string[] candidateNames);
}

public static class MarkedTypeDeclarationCandidateProvider
{
    public delegate TOut DOutputTransform<out TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax);

    public static IMarkedTypeDeclarationCandidateProvider<TOut> Construct<TOut>(DOutputTransform<TOut> outputTransform) => new Provider<TOut>(outputTransform);
    public static IMarkedTypeDeclarationCandidateProvider<TypeDeclarationSyntax> Construct()
    {
        return Construct(extractDeclaration);

        static TypeDeclarationSyntax extractDeclaration(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax) => declaration;
    }

    private sealed class Provider<TOut> : IMarkedTypeDeclarationCandidateProvider<TOut>
    {
        public DOutputTransform<TOut> OutputTransform { get; }

        public Provider(DOutputTransform<TOut> outputTransform)
        {
            OutputTransform = outputTransform;
        }

        public IncrementalValuesProvider<Optional<TOut>> Attach<TAttribute>(SyntaxValueProvider syntaxProvider) => Attach(syntaxProvider, typeof(TAttribute));
        public IncrementalValuesProvider<Optional<TOut>> Attach(SyntaxValueProvider syntaxProvider, Type attributeType) => Attach(syntaxProvider, attributeType.FullName);

        public IncrementalValuesProvider<Optional<TOut>> Attach(SyntaxValueProvider syntaxProvider, string attributeName)
        {
            IAttributeSyntaxStrategy attributeStrategy = new AttributeSyntaxFromName(attributeName);

            return Attach(attributeStrategy, syntaxProvider);
        }

        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf<TAttribute1, TAttribute2>(SyntaxValueProvider syntaxProvider) => AttachAnyOf(syntaxProvider, typeof(TAttribute1), typeof(TAttribute2));
        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf<TAttribute1, TAttribute2, TAttribute3>(SyntaxValueProvider syntaxProvider) => AttachAnyOf(syntaxProvider, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));

        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<Type> candidateTypes)
        {
            return AttachAnyOf(syntaxProvider, candidateNames());

            IEnumerable<string> candidateNames()
            {
                foreach (Type candidateType in candidateTypes)
                {
                    yield return candidateType.FullName;
                }
            }
        }

        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params Type[] candidateTypes) => AttachAnyOf(syntaxProvider, candidateTypes as IEnumerable<Type>);
        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params string[] candidateNames) => AttachAnyOf(syntaxProvider, candidateNames as IEnumerable<string>);
        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<string> candidateNames)
        {
            IAttributeSyntaxStrategy attributeStrategy = new AttributeSyntaxFromFirstName(candidateNames);

            return Attach(attributeStrategy, syntaxProvider);
        }

        private interface IAttributeSyntaxStrategy
        {
            public abstract AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration);
        }

        private sealed class AttributeSyntaxFromName : IAttributeSyntaxStrategy
        {
            private string AttributeName { get; }

            public AttributeSyntaxFromName(string attributeName)
            {
                AttributeName = attributeName;
            }

            public AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration) => declaration.GetAttributeWithName(context.SemanticModel, AttributeName);
        }

        private sealed class AttributeSyntaxFromFirstName : IAttributeSyntaxStrategy
        {
            private IEnumerable<string> CandidateNames { get; }

            public AttributeSyntaxFromFirstName(IEnumerable<string> candidateNames)
            {
                CandidateNames = candidateNames;
            }

            public AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration) => declaration.GetFirstAttributeWithName(context.SemanticModel, CandidateNames);
        }

        private IncrementalValuesProvider<Optional<TOut>> Attach(IAttributeSyntaxStrategy attributeStrategy, SyntaxValueProvider syntaxProvider)
        {
            return syntaxProvider.CreateSyntaxProvider(
                predicate: SyntaxNodeIsTypeDeclarationWithAttributes,
                transform: candidateTypeDeclarationElseNull
            ).Select(ApplyOutputTransform);

            OutputData? candidateTypeDeclarationElseNull(GeneratorSyntaxContext context, CancellationToken token) => CandidateTypeDeclarationElseNull(attributeStrategy, context, token);
        }

        private Optional<TOut> ApplyOutputTransform(OutputData? result, CancellationToken _)
        {
            if (result is null)
            {
                return new Optional<TOut>();
            }

            return OutputTransform(result.Value.Declaration, result.Value.AttributeSyntax);
        }

        private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node, CancellationToken _) => node is TypeDeclarationSyntax declaration && declaration.Identifier.IsMissing is false && declaration.AttributeLists.Count > 0;
        private static OutputData? CandidateTypeDeclarationElseNull(IAttributeSyntaxStrategy attributeStrategy, GeneratorSyntaxContext context, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return null;
            }

            TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

            if (attributeStrategy.GetAttributeSyntax(context, declaration) is AttributeSyntax attributeSyntax)
            {
                return new OutputData(declaration, attributeSyntax);
            }

            return null;
        }

        private readonly record struct OutputData(TypeDeclarationSyntax Declaration, AttributeSyntax AttributeSyntax);
    }
}
