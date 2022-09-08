namespace SharpMeasures.Generators.Providers.DeclarationFilter;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Diagnostics;

using System;
using System.Collections.Generic;
using System.Threading;

public interface IDeclarationFilter
{
    public abstract bool CheckValidity(BaseTypeDeclarationSyntax declaration);
    public abstract Diagnostic? ProduceDiagnostics(BaseTypeDeclarationSyntax declaration);
}

public interface IFilteredDeclarationProvider<TData>
{
    public abstract IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider);
    public abstract IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TData> inputProvider);
}

public static class FilteredDeclarationProvider
{
    public delegate BaseTypeDeclarationSyntax DInputTransform<in TData>(TData input);

    public delegate Type DAttributeTypeTransform<in TData>(TData input);
    public delegate string DAttributeNameTransform<in TData>(TData input);

    public static IFilteredDeclarationProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform, IEnumerable<IDeclarationFilter> filters)
    {
        return new ImmediateProvider<TData>(inputTransform, filters);
    }

    public static IFilteredDeclarationProvider<TDeclaration> Construct<TDeclaration>(IEnumerable<IDeclarationFilter> filters)
        where TDeclaration : BaseTypeDeclarationSyntax
    {
        return Construct<TDeclaration>(ExtractDeclaration, filters);
    }

    private static BaseTypeDeclarationSyntax ExtractDeclaration<TDeclaration>(TDeclaration declaration) where TDeclaration : BaseTypeDeclarationSyntax => declaration;

    private class ImmediateProvider<TData> : IFilteredDeclarationProvider<TData>
    {
        private DInputTransform<TData> InputTransform { get; }
        private IEnumerable<IDeclarationFilter> Filters { get; }

        public ImmediateProvider(DInputTransform<TData> inputTransform, IEnumerable<IDeclarationFilter> filters)
        {
            InputTransform = inputTransform;
            Filters = filters;
        }

        public IncrementalValuesProvider<TData> AttachWithoutDiagnostics(IncrementalValuesProvider<TData> inputProvider)
        {
            return inputProvider.Where(declarationIsValid);

            bool declarationIsValid(TData input)
            {
                var declaration = InputTransform(input);

                foreach (var filter in Filters)
                {
                    if (filter.CheckValidity(declaration) is false)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public IncrementalValuesProvider<TData> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<TData> inputProvider)
        {
            return inputProvider.Select(Process).ReportDiagnostics(context);
        }

        protected IOptionalWithDiagnostics<TData> Process(TData input, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return OptionalWithDiagnostics.Empty<TData>();
            }

            var declaration = InputTransform(input);

            foreach (var filter in Filters)
            {
                if (filter.CheckValidity(declaration) is false)
                {
                    return OptionalWithDiagnostics.Empty<TData>(filter.ProduceDiagnostics(declaration));
                }
            }

            return OptionalWithDiagnostics.Result(input);
        }
    }
}
