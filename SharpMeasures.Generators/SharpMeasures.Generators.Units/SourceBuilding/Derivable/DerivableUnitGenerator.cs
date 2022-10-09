namespace SharpMeasures.Generators.Units.SourceBuilding.Derivable;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Units.DataModel>> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(ReduceToDataModel);

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static Optional<DataModel> ReduceToDataModel(Optional<Units.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Unit.UnitDerivations.Any() is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Unit.Type, model.Value.Unit.Definition.Quantity, model.Value.UnitPopulation, model.Value.Unit.UnitDerivations, model.Value.SourceBuildingContext);
    }
}
