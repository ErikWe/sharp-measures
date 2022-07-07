namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        return new(input.UnitData.Type, input.UnitData.UnitDefinition.Quantity.Type, input.UnitData.UnitDefinition.BiasTerm, input.UnitData.FixedUnit,
            input.UnitData.UnitAliases, input.UnitData.DerivedUnits, input.UnitData.BiasedUnits, input.UnitData.PrefixedUnits, input.UnitData.ScaledUnits, input.Documentation);
    }
}
