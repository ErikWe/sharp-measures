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
    public abstract IncrementalValuesProvider<Optional<TData>> AttachWithoutDiagnostics(IncrementalValuesProvider<Optional<TData>> inputProvider);
    public abstract IncrementalValuesProvider<Optional<TData>> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<TData>> inputProvider);
}

public static class FilteredDeclarationProvider
{
    public delegate BaseTypeDeclarationSyntax DInputTransform<in TData>(TData input);

    public delegate Type DAttributeTypeTransform<in TData>(TData input);
    public delegate string DAttributeNameTransform<in TData>(TData input);

    public static IFilteredDeclarationProvider<TData> Construct<TData>(DInputTransform<TData> inputTransform, IEnumerable<IDeclarationFilter> filters) => new ImmediateProvider<TData>(inputTransform, filters);
    public static IFilteredDeclarationProvider<TDeclaration> Construct<TDeclaration>(IEnumerable<IDeclarationFilter> filters) where TDeclaration : BaseTypeDeclarationSyntax => Construct<TDeclaration>(ExtractDeclaration, filters);

    private static BaseTypeDeclarationSyntax ExtractDeclaration<TDeclaration>(TDeclaration declaration) where TDeclaration : BaseTypeDeclarationSyntax => declaration;

    private sealed class ImmediateProvider<TData> : IFilteredDeclarationProvider<TData>
    {
        private DInputTransform<TData> InputTransform { get; }
        private IEnumerable<IDeclarationFilter> Filters { get; }

        public ImmediateProvider(DInputTransform<TData> inputTransform, IEnumerable<IDeclarationFilter> filters)
        {
            InputTransform = inputTransform;
            Filters = filters;
        }

        public IncrementalValuesProvider<Optional<TData>> AttachWithoutDiagnostics(IncrementalValuesProvider<Optional<TData>> inputProvider)
        {
            return inputProvider.Select(declarationIsValid);

            Optional<TData> declarationIsValid(Optional<TData> input, CancellationToken _)
            {
                if (input.HasValue is false)
                {
                    return input;
                }

                var declaration = InputTransform(input.Value);

                foreach (var filter in Filters)
                {
                    if (filter.CheckValidity(declaration) is false)
                    {
                        return new Optional<TData>();
                    }
                }

                return input;
            }
        }

        public IncrementalValuesProvider<Optional<TData>> AttachAndReport(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<TData>> inputProvider) => inputProvider.Select(Process).ReportDiagnostics(context);

        private IOptionalWithDiagnostics<TData> Process(Optional<TData> input, CancellationToken token)
        {
            if (token.IsCancellationRequested || input.HasValue is false)
            {
                return OptionalWithDiagnostics.Empty<TData>();
            }

            var declaration = InputTransform(input.Value);

            foreach (var filter in Filters)
            {
                if (filter.CheckValidity(declaration) is false)
                {
                    return OptionalWithDiagnostics.Empty<TData>(filter.ProduceDiagnostics(declaration));
                }
            }

            return OptionalWithDiagnostics.Result(input.Value);
        }
    }
}
