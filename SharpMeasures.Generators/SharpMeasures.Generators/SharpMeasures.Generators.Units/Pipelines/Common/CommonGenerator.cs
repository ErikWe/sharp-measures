namespace SharpMeasures.Generators.Units.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

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
        return new(input.UnitData.UnitType, input.UnitDefinition.Quantity.ScalarType, input.UnitDefinition.BiasTerm, input.Documentation,
            SourceBuildingUtility.ToParameterName(input.UnitDefinition.Quantity.ScalarType.Name));
    }
}
