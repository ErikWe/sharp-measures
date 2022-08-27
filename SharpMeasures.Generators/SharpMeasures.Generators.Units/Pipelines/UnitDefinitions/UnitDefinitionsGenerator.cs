namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> modelProvider)
    {
        var filteredAnRreduced = modelProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(filteredAnRreduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Units.DataModel model, CancellationToken _)
    {
        if (model.Unit.FixedUnit is null && model.Unit.UnitAliases.Any() is false && model.Unit.DerivedUnits.Any() is false && model.Unit.BiasedUnits.Any() is false && model.Unit.PrefixedUnits.Any() is false
            && model.Unit.ScaledUnits.Any() is false)
        {
            return null;
        }

        return new(model.Unit.Type, model.Unit.Definition.Quantity, model.Unit.Definition.BiasTerm, model.Unit.FixedUnit, model.Unit.UnitAliases, model.Unit.DerivedUnits, model.Unit.BiasedUnits,
            model.Unit.PrefixedUnits, model.Unit.ScaledUnits, model.Unit.DerivationsByID, model.Documentation);
    }
}
