namespace SharpMeasures.Generators.Units.Pipelines.Comparable;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var unbiasedAndReduced = inputProvider.Where(DisallowsBias).Select(ReduceToDataModel);

        context.RegisterSourceOutput(unbiasedAndReduced, Execution.Execute);
    }

    private static bool DisallowsBias(Units.DataModel input) => input.Unit.UnitDefinition.SupportsBiasedQuantities is false;

    private static DataModel ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        return new(input.Unit.UnitType, input.Quantity.ScalarType.AsNamedType(), input.Documentation);
    }
}
