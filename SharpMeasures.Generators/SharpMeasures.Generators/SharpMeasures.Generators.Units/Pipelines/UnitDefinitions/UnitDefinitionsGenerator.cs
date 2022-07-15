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
        return new(input.Unit.Type, input.Unit.Definition.Quantity.Type, input.Unit.Definition.BiasTerm, input.Unit.FixedUnit,
            input.Unit.UnitAliases, input.Unit.DerivedUnits, input.Unit.BiasedUnits, input.Unit.PrefixedUnits, input.Unit.ScaledUnits, input.Documentation);
    }
}
