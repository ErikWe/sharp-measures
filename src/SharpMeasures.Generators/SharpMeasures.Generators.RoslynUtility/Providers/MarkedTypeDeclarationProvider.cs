namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Threading;

public readonly record struct MarkedTypeDeclarationConfiguration(bool AllowAliases)
{
    public static MarkedTypeDeclarationConfiguration Default { get; } = new(true);
}

public interface IMarkedTypeDeclarationProvider<TOut>
{
    public abstract IMarkedTypeDeclarationProvider<TOut> RegisterConfigurationProvider(IncrementalValueProvider<MarkedTypeDeclarationConfiguration> configurationProvider);

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

public static class MarkedTypeDeclarationProvider
{
    public delegate TOut DOutputTransform<out TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax);
    public delegate TOut DTargetedOutputTransform<out TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax, string targetAttribute);

    public static IMarkedTypeDeclarationProvider<TOut> Construct<TOut>(DOutputTransform<TOut> outputTransform)
    {
        return new Provider<TOut>(targetedOutputTransform);

        TOut targetedOutputTransform(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax, string targetAttribute) => outputTransform(declaration, attributeSyntax);
    }

    public static IMarkedTypeDeclarationProvider<TypeDeclarationSyntax> Construct()
    {
        return Construct(extractDeclaration);

        static TypeDeclarationSyntax extractDeclaration(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax) => declaration;
    }

    public static IMarkedTypeDeclarationProvider<TOut> Construct<TOut>(DTargetedOutputTransform<TOut> outputTransform) => new Provider<TOut>(outputTransform);
    public static IMarkedTypeDeclarationProvider<(TypeDeclarationSyntax Declaration, string Attribute)> ConstructTargeted()
    {
        return Construct(extract);

        static (TypeDeclarationSyntax, string) extract(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax, string targetAttribute) => (declaration, targetAttribute);
    }

    private sealed class Provider<TOut> : IMarkedTypeDeclarationProvider<TOut>
    {
        private Optional<IncrementalValueProvider<MarkedTypeDeclarationConfiguration>> ConfigurationProvider { get; set; }

        public DTargetedOutputTransform<TOut> OutputTransform { get; }

        public Provider(DTargetedOutputTransform<TOut> outputTransform)
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

        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params Type[] candidateTypes) => AttachAnyOf(syntaxProvider, candidateTypes as IEnumerable<Type>);
        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<Type> candidateTypes)
        {
            return AttachAnyOf(syntaxProvider, candidateNames());

            IEnumerable<string> candidateNames()
            {
                foreach (var candidateType in candidateTypes)
                {
                    yield return candidateType.FullName;
                }
            }
        }

        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, params string[] candidateNames) => AttachAnyOf(syntaxProvider, candidateNames as IEnumerable<string>);
        public IncrementalValuesProvider<Optional<TOut>> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<string> candidateNames)
        {
            IAttributeSyntaxStrategy attributeStrategy = new AttributeSyntaxFromAnyName(candidateNames);

            return Attach(attributeStrategy, syntaxProvider);
        }

        public IMarkedTypeDeclarationProvider<TOut> RegisterConfigurationProvider(IncrementalValueProvider<MarkedTypeDeclarationConfiguration> configurationProvider)
        {
            ConfigurationProvider = configurationProvider;

            return this;
        }

        private interface IAttributeSyntaxStrategy
        {
            public abstract (AttributeSyntax Syntax, string TargetAttribute)? GetAttributeSyntax(SemanticModel semanticModel, TypeDeclarationSyntax declaration, bool checkForAlias);
        }

        private sealed class AttributeSyntaxFromName : IAttributeSyntaxStrategy
        {
            private string AttributeName { get; }

            public AttributeSyntaxFromName(string attributeName)
            {
                AttributeName = attributeName;
            }

            public (AttributeSyntax, string)? GetAttributeSyntax(SemanticModel semanticModel, TypeDeclarationSyntax declaration, bool checkForAlias)
            {
                if (declaration.GetAttributeWithName(semanticModel, AttributeName, checkForAlias) is AttributeSyntax syntax)
                {
                    return (syntax, AttributeName);
                }

                return null;
            }
        }

        private sealed class AttributeSyntaxFromAnyName : IAttributeSyntaxStrategy
        {
            private IEnumerable<string> CandidateNames { get; }

            public AttributeSyntaxFromAnyName(IEnumerable<string> candidateNames)
            {
                CandidateNames = candidateNames;
            }

            public (AttributeSyntax, string)? GetAttributeSyntax(SemanticModel semanticModel, TypeDeclarationSyntax declaration, bool checkForAlias) => declaration.GetAttributeWithAnyName(semanticModel, CandidateNames, checkForAlias);
        }

        private IncrementalValuesProvider<Optional<TOut>> Attach(IAttributeSyntaxStrategy attributeStrategy, SyntaxValueProvider syntaxProvider)
        {
            var declarations = syntaxProvider.CreateSyntaxProvider(SyntaxNodeIsTypeDeclarationWithAttributes, CandidateTypeDeclaration);

            if (ConfigurationProvider.HasValue)
            {
                return declarations.Combine(ConfigurationProvider.Value).Select(selectWithRegistered).Select(ApplyOutputTransform);
            }

            return declarations.Select(selectWithDefault).Select(ApplyOutputTransform);

            Optional<OutputData> selectWithRegistered(((SemanticModel SemanticModel, TypeDeclarationSyntax Declaration) Candidate, MarkedTypeDeclarationConfiguration Configuration) input, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return new Optional<OutputData>();
                }

                return MatchingTypeDeclaration(attributeStrategy, input.Configuration, input.Candidate.SemanticModel, input.Candidate.Declaration);
            }

            Optional<OutputData> selectWithDefault((SemanticModel SemanticModel, TypeDeclarationSyntax Declaration) input, CancellationToken token)
            {
                if (token.IsCancellationRequested)
                {
                    return new Optional<OutputData>();
                }

                return MatchingTypeDeclaration(attributeStrategy, MarkedTypeDeclarationConfiguration.Default, input.SemanticModel, input.Declaration);
            }
        }

        private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node, CancellationToken _) => node is TypeDeclarationSyntax declaration && declaration.Identifier.IsMissing is false && declaration.AttributeLists.Count > 0;
        private static (SemanticModel SemanticModel, TypeDeclarationSyntax Declaration) CandidateTypeDeclaration(GeneratorSyntaxContext context, CancellationToken _) => (context.SemanticModel, (TypeDeclarationSyntax)context.Node);

        private static Optional<OutputData> MatchingTypeDeclaration(IAttributeSyntaxStrategy attributeStrategy, MarkedTypeDeclarationConfiguration configuration, SemanticModel semanticModel, TypeDeclarationSyntax declaration)
        {
            if (attributeStrategy.GetAttributeSyntax(semanticModel, declaration, configuration.AllowAliases) is (AttributeSyntax, string) result)
            {
                return new OutputData(declaration, result.Syntax, result.TargetAttribute);
            }

            return new Optional<OutputData>();
        }

        private Optional<TOut> ApplyOutputTransform(Optional<OutputData> result, CancellationToken _)
        {
            if (result.HasValue is false)
            {
                return new Optional<TOut>();
            }

            return OutputTransform(result.Value.Declaration, result.Value.AttributeSyntax, result.Value.TargetAttribute);
        }

        private readonly record struct OutputData(TypeDeclarationSyntax Declaration, AttributeSyntax AttributeSyntax, string TargetAttribute);
    }
}
