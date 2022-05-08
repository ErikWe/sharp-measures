namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Threading;

internal static class MarkedTypeDeclarationCandidateProvider
{
    public delegate TOut DOutputTransform<TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax);

    public static IncrementalValuesProvider<TOut> Attach<TOut, TAttribute>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform)
    {
        return Attach(syntaxProvider, typeof(TAttribute), outputTransform);
    }

    public static IncrementalValuesProvider<TOut> Attach<TOut>(SyntaxValueProvider syntaxProvider, Type attributeType,
        DOutputTransform<TOut> outputTransform)
    {
        return Attach(syntaxProvider, attributeType.FullName, outputTransform);
    }

    public static IncrementalValuesProvider<TOut> Attach<TOut>(SyntaxValueProvider syntaxProvider, string attributeName,
        DOutputTransform<TOut> outputTransform)
    {
        Provider<TOut> provider = new(outputTransform, attributeName);

        return provider.Attach(syntaxProvider);
    }

    public static IncrementalValuesProvider<TOut> AttachFirst<TOut>(SyntaxValueProvider syntaxProvider, IReadOnlyCollection<Type> attributeTypes,
        DOutputTransform<TOut> outputTransform)
    {
        string[] attributeNames = new string[attributeTypes.Count];

        int index = 0;
        foreach (Type attributeType in attributeTypes)
        {
            attributeNames[index++] = attributeType.FullName;
        }

        return AttachFirst(syntaxProvider, attributeNames, outputTransform);
    }

    public static IncrementalValuesProvider<TOut> AttachFirst<TOut>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform,
        params Type[] attributeTypes)
    {
        return AttachFirst(syntaxProvider, attributeTypes, outputTransform);
    }

    public static IncrementalValuesProvider<TOut> AttachFirst<TOut>(SyntaxValueProvider syntaxProvider, IReadOnlyCollection<string> attributeNames,
        DOutputTransform<TOut> outputTransform)
    {
        FirstAttributeProvider<TOut> provider = new(outputTransform, attributeNames);

        return provider.Attach(syntaxProvider);
    }

    public static IncrementalValuesProvider<TOut> AttachFirst<TOut>(SyntaxValueProvider syntaxProvider, DOutputTransform<TOut> outputTransform,
        params string[] attributeNames)
    {
        return AttachFirst(syntaxProvider, attributeNames, outputTransform);
    }

    private abstract class AProvider<TOut>
    {
        private DOutputTransform<TOut> OutputTransform { get; }

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

    private class Provider<TOut> : AProvider<TOut>
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

    private class FirstAttributeProvider<TOut> : AProvider<TOut>
    {
        private IReadOnlyCollection<string> AttributeNames { get; }

        public FirstAttributeProvider(DOutputTransform<TOut> outputTransform, IReadOnlyCollection<string> attributeNames) : base(outputTransform)
        {
            AttributeNames = attributeNames;
        }

        protected override AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration)
        {
            return declaration.GetFirstAttributeWithNameIn(AttributeNames, context.SemanticModel);
        }
    }
}
