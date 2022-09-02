namespace SharpMeasures.Generators.Units.Pipelines.UnitInstances;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitInstancesGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> modelProvider)
    {
        var filteredAnRreduced = modelProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(filteredAnRreduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Units.DataModel model, CancellationToken _)
    {
        if (model.Unit.FixedUnitInstance is null && model.Unit.UnitInstanceAliases.Any() is false && model.Unit.DerivedUnitInstances.Any() is false && model.Unit.BiasedUnitInstances.Any() is false && model.Unit.PrefixedUnitInstances.Any() is false
            && model.Unit.ScaledUnitInstances.Any() is false)
        {
            return null;
        }

        return new(model.Unit.Type, model.Unit.Definition.Quantity, model.Unit.Definition.BiasTerm, model.Unit.FixedUnitInstance, model.Unit.UnitInstanceAliases, model.Unit.DerivedUnitInstances, model.Unit.BiasedUnitInstances,
            model.Unit.PrefixedUnitInstances, model.Unit.ScaledUnitInstances, model.Unit.DerivationsByID, model.Documentation);
    }
}
