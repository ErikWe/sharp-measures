namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Diagnostics;
using SharpMeasures.Generators.Scalars.Refinement;

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
        DimensionalEquivalenceRefinementContext context = new(input.Unit.UnitType, input.Scalar.ScalarDefinition.Biased, input.ScalarPopulation);
        DimensionalEquivalenceRefiner refiner = new(DimensionalEquivalenceDiagnostics.Instance);

        var processed = ProcessingFilter.Create(refiner).Filter(context, input.Scalar.DimensionalEquivalences);

        DataModel model = new(input.Scalar.ScalarType, processed.Result, input.Documentation);
        return ResultWithDiagnostics.Construct(model, processed.Diagnostics);
    }

    private readonly record struct DimensionalEquivalenceRefinementContext : IDimensionalEquivalenceRefinementContext
    {
        public DefinedType Type { get; }

        public bool Biased { get; }
        public ScalarPopulation ScalarPopulation { get; }

        public DimensionalEquivalenceRefinementContext(DefinedType type, bool biased, ScalarPopulation scalarPopulation)
        {
            Type = type;

            Biased = biased;
            ScalarPopulation = scalarPopulation;
        }
    }
}
