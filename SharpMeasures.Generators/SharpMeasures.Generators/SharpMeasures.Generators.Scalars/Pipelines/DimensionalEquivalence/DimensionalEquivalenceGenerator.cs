namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Refinement.DimensionalEquivalence;

using System.Threading;
using System.Linq;

internal static class DimensionalEquivalenceGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> ReduceToDataModel(Scalars.DataModel input, CancellationToken _)
    {
        DimensionalEquivalenceRefinementContext context = new(input.ScalarDefinition.Unit.Type, input.ScalarData.ScalarDefinition.UseUnitBias, input.ScalarPopulation);
        ConvertibleQuantityRefiner refiner = new(ConvertibleQuantityDiagnostics.Instance);

        var processed = ProcessingFilter.Create(refiner).Filter(context, input.ScalarData.convertibleQuantities);

        DataModel model = new(input.ScalarData.ScalarType, processed.Result, input.Documentation);
        return ResultWithDiagnostics.Construct(model, processed.Diagnostics);
    }

    private readonly record struct DimensionalEquivalenceRefinementContext : IConvertibleQuantityRefinementContext
    {
        public DefinedType Type { get; }

        public bool Biased { get; }
        public IScalarPopulation ScalarPopulation { get; }

        public DimensionalEquivalenceRefinementContext(DefinedType type, bool biased, IScalarPopulation scalarPopulation)
        {
            Type = type;

            Biased = biased;
            ScalarPopulation = scalarPopulation;
        }
    }
}
