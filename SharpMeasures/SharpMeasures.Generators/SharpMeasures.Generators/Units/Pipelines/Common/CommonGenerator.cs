namespace SharpMeasures.Generators.Units.Pipelines.Common;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        return new(input.Unit.UnitType, input.Quantity.ScalarType.AsNamedType(), input.Unit.UnitDefinition.SupportsBiasedQuantities, input.Documentation);
    }
}
