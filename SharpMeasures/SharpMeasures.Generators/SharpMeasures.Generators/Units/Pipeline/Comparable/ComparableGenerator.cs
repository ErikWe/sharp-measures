namespace SharpMeasures.Generators.Units.Pipeline.Comparable;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var reducedUnbiased = inputProvider.Where(DisallowsBias).Select(ReduceToDataModel);

        context.RegisterSourceOutput(reducedUnbiased, Execution.Execute);
    }

    private static bool DisallowsBias(Units.DataModel input) => input.Unit.UnitDefinition.AllowBias is false;

    private static DataModel ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        return new(input.Unit.UnitType, input.Quantity.ScalarType, input.Documentation);
    }
}
