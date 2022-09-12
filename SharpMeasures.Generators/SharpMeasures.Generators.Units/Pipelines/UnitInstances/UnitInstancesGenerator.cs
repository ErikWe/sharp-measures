namespace SharpMeasures.Generators.Units.Pipelines.UnitInstances;

using Microsoft.CodeAnalysis;

using System.Linq;
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
        if (model.HasValue is false || model.Value.Unit.FixedUnitInstance is null && model.Value.Unit.UnitInstanceAliases.Any() is false && model.Value.Unit.DerivedUnitInstances.Any() is false
            && model.Value.Unit.BiasedUnitInstances.Any() is false && model.Value.Unit.PrefixedUnitInstances.Any() is false && model.Value.Unit.ScaledUnitInstances.Any() is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Unit.Type, model.Value.Unit.Definition.Quantity, model.Value.Unit.Definition.BiasTerm, model.Value.Unit.FixedUnitInstance, model.Value.Unit.UnitInstanceAliases,
            model.Value.Unit.DerivedUnitInstances, model.Value.Unit.BiasedUnitInstances, model.Value.Unit.PrefixedUnitInstances, model.Value.Unit.ScaledUnitInstances, model.Value.Unit.DerivationsByID, model.Value.Documentation);
    }
}
