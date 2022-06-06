namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Units.Processing;

using System.Threading;
using System.Linq;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(FilterAndReduceToDataModel).ReportDiagnostics(context);

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static IResultWithDiagnostics<DataModel> FilterAndReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        DerivableUnitProcessingContext context = new(input.Unit.UnitType, input.UnitPopulation);
        var processed = ProcessingFilter.Create(DerivableUnitProcesser.Instance).Filter(context, input.Unit.UnitDerivations);

        DataModel model = new(input.Unit.UnitType, input.Quantity.ScalarType.AsNamedType(), input.Documentation, processed.Result);
        return ResultWithDiagnostics.Construct(model, processed.Diagnostics);
    }
}
