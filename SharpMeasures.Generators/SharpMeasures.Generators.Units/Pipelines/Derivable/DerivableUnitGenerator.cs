namespace SharpMeasures.Generators.Units.Pipelines.Derivable;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Units.DataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(ReduceToDataModel).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? ReduceToDataModel(Units.DataModel model, CancellationToken _)
    {
        if (model.Unit.UnitDerivations.Any() is false)
        {
            return null;
        }

        return new(model.Unit.Type, model.Unit.Definition.Quantity, model.UnitPopulation, model.Unit.UnitDerivations, model.Documentation);
    }
}
