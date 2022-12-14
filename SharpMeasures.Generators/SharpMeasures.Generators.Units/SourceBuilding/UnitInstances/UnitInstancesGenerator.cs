namespace SharpMeasures.Generators.Units.SourceBuilding.UnitInstances;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class UnitInstancesGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Units.DataModel>> modelProvider)
    {
        var filteredAnRreduced = modelProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(filteredAnRreduced, Execution.Execute);
    }

    private static Optional<DataModel> ReduceToDataModel(Optional<Units.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || (model.Value.Unit.FixedUnitInstance is null && model.Value.Unit.UnitInstanceAliases.Count is 0 && model.Value.Unit.DerivedUnitInstances.Count is 0 && model.Value.Unit.BiasedUnitInstances.Count is 0 && model.Value.Unit.PrefixedUnitInstances.Count is 0 && model.Value.Unit.ScaledUnitInstances.Count is 0))
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Unit.Type, model.Value.Unit.Definition.Quantity, model.Value.Unit.Definition.BiasTerm, model.Value.Unit.FixedUnitInstance, model.Value.Unit.UnitInstanceAliases, model.Value.Unit.DerivedUnitInstances, model.Value.Unit.BiasedUnitInstances,
            model.Value.Unit.PrefixedUnitInstances, model.Value.Unit.ScaledUnitInstances, model.Value.Unit.DerivationsByID, model.Value.SourceBuildingContext);
    }
}
