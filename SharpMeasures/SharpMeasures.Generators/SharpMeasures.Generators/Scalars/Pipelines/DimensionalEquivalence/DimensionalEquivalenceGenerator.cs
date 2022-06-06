namespace SharpMeasures.Generators.Scalars.Pipelines.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Processing;

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
        DimensionalEquivalenceProcessingContext context = new(input.Scalar.ScalarType, input.ScalarPopulation);
        var processed = ProcessingFilter.Create(DimensionalEquivalenceReprocesser.Instance).Filter(context, input.Scalar.DimensionalEquivalences,
            new ProcessedDimensionalEquivalence());

        DataModel model = new(input.Scalar.ScalarType, processed.Result, input.Documentation);
        return ResultWithDiagnostics.Construct(model, processed.Diagnostics);
    }
}
