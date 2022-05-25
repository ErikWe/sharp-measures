﻿namespace SharpMeasures.Generators.Providers;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System;
using System.Collections.Generic;
using System.Threading;

internal static class MarkedTypeDeclarationCandidateProvider
{
    public delegate TOut DOutputTransform<out TOut>(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax);

    public static IProvider<TOut> Construct<TOut>(DOutputTransform<TOut> outputTransform)
    {
        return new Provider<TOut>(outputTransform);
    }

    public static IProvider<TypeDeclarationSyntax> Construct()
    {
        return Construct(extractDeclaration);

        static TypeDeclarationSyntax extractDeclaration(TypeDeclarationSyntax declaration, AttributeSyntax attributeSyntax) => declaration;
    }

    public interface IProvider<TOut>
    {
        public abstract IncrementalValuesProvider<TOut> Attach<TAttribute>(SyntaxValueProvider syntaxProvider);
        public abstract IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, Type attributeType);
        public abstract IncrementalValuesProvider<TOut> Attach(SyntaxValueProvider syntaxProvider, string attributeName);

        public abstract IncrementalValuesProvider<TOut> AttachAnyOf<TAttribute1, TAttribute2>(SyntaxValueProvider syntaxProvider);
        public abstract IncrementalValuesProvider<TOut> AttachAnyOf<TAttribute1, TAttribute2, TAttribute3>(SyntaxValueProvider syntaxProvider);

        public abstract IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<Type> candidateTypes);
        public abstract IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, params Type[] candidateTypes);

        public abstract IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<string> candidateNames);
        public abstract IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, params string[] candidateNames);
    }

    private class Provider<TOut> : IProvider<TOut>
    {
        public DOutputTransform<TOut> OutputTransform { get; }

        public Provider(DOutputTransform<TOut> outputTransform)
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
            IAttributeSyntaxStrategy attributeStrategy = new AttributeSyntaxFromName(attributeName);

            return Attach(attributeStrategy, syntaxProvider);
        }

        public IncrementalValuesProvider<TOut> AttachAnyOf<TAttribute1, TAttribute2>(SyntaxValueProvider syntaxProvider)
        {
            return AttachAnyOf(syntaxProvider, typeof(TAttribute1), typeof(TAttribute2));
        }

        public IncrementalValuesProvider<TOut> AttachAnyOf<TAttribute1, TAttribute2, TAttribute3>(SyntaxValueProvider syntaxProvider)
        {
            return AttachAnyOf(syntaxProvider, typeof(TAttribute1), typeof(TAttribute2), typeof(TAttribute3));
        }

        public IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<Type> candidateTypes)
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

        public IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, params Type[] candidateTypes)
        {
            return AttachAnyOf(syntaxProvider, candidateTypes as IEnumerable<Type>);
        }

        public IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, IEnumerable<string> candidateNames)
        {
            IAttributeSyntaxStrategy attributeStrategy = new AttributeSyntaxFromFirstName(candidateNames);

            return Attach(attributeStrategy, syntaxProvider);
        }

        public IncrementalValuesProvider<TOut> AttachAnyOf(SyntaxValueProvider syntaxProvider, params string[] candidateNames)
        {
            return AttachAnyOf(syntaxProvider, candidateNames as IEnumerable<string>);
        }

        private interface IAttributeSyntaxStrategy
        {
            public abstract AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration);
        }

        private class AttributeSyntaxFromName : IAttributeSyntaxStrategy
        {
            private string AttributeName { get; }

            public AttributeSyntaxFromName(string attributeName)
            {
                AttributeName = attributeName;
            }

            public AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration)
            {
                return declaration.GetAttributeWithName(context.SemanticModel, AttributeName);
            }
        }

        private class AttributeSyntaxFromFirstName : IAttributeSyntaxStrategy
        {
            private IEnumerable<string> CandidateNames { get; }

            public AttributeSyntaxFromFirstName(IEnumerable<string> candidateNames)
            {
                CandidateNames = candidateNames;
            }

            public AttributeSyntax? GetAttributeSyntax(GeneratorSyntaxContext context, TypeDeclarationSyntax declaration)
            {
                return declaration.GetFirstAttributeWithName(context.SemanticModel, CandidateNames);
            }
        }

        private IncrementalValuesProvider<TOut> Attach(IAttributeSyntaxStrategy attributeStrategy, SyntaxValueProvider syntaxProvider)
        {
            return syntaxProvider.CreateSyntaxProvider(
                predicate: SyntaxNodeIsTypeDeclarationWithAttributes,
                transform: candidateTypeDeclarationElseNull
            ).WhereNotNull().Select(ApplyOutputTransform);

            OutputData? candidateTypeDeclarationElseNull(GeneratorSyntaxContext context, CancellationToken _)
                => CandidateTypeDeclarationElseNull(attributeStrategy, context);
        }

        private TOut ApplyOutputTransform(OutputData result, CancellationToken _)
        {
            return OutputTransform(result.Declaration, result.AttributeSyntax);
        }

        private static bool SyntaxNodeIsTypeDeclarationWithAttributes(SyntaxNode node, CancellationToken _)
        {
            return node is TypeDeclarationSyntax declaration &&!declaration.Identifier.IsMissing is false && declaration.AttributeLists.Count > 0;
        }

        private static OutputData? CandidateTypeDeclarationElseNull(IAttributeSyntaxStrategy attributeStrategy, GeneratorSyntaxContext context)
        {
            TypeDeclarationSyntax declaration = (TypeDeclarationSyntax)context.Node;

            return attributeStrategy.GetAttributeSyntax(context, declaration) is AttributeSyntax attributeSyntax ? new OutputData(declaration, attributeSyntax) : null;
        }

        private readonly record struct OutputData(TypeDeclarationSyntax Declaration, AttributeSyntax AttributeSyntax);
    }
}
