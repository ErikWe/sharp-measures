namespace SharpMeasures.Generators.Units.Pipelines.UnitDefinitions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> inputProvider)
    {
        var filteredAnRreduced = inputProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(filteredAnRreduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Units.DataModel input, CancellationToken _)
    {
        if (input.Unit.FixedUnit is null && input.Unit.UnitAliases.Any() is false && input.Unit.DerivedUnits.Any() is false && input.Unit.BiasedUnits.Any() is false && input.Unit.PrefixedUnits.Any() is false
            && input.Unit.ScaledUnits.Any() is false)
        {
            return null;
        }

        return new(input.Unit.Type, input.Unit.Definition.Quantity.Type, input.Unit.Definition.BiasTerm, input.Unit.FixedUnit, input.Unit.UnitAliases, input.Unit.DerivedUnits, input.Unit.BiasedUnits,
            input.Unit.PrefixedUnits, input.Unit.ScaledUnits, input.Documentation);
    }
}
