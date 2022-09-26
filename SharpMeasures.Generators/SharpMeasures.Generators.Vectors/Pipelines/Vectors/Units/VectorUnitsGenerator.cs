namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorUnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Vector.IncludedUnitInstanceNames.Count is 0 && model.Value.Vector.Constants.Count is 0)
        {
            return new Optional<DataModel>();
        }

        var unit = model.Value.UnitPopulation.Units[model.Value.Vector.Unit];

        var includedUnits = model.Value.Vector.IncludedUnitInstanceNames.Select((unitName) => unit.UnitInstancesByName[unitName]).ToList();

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Scalar, model.Value.Vector.Unit, unit.Definition.Quantity, includedUnits, model.Value.Vector.Constants, model.Value.Documentation);
    }
}
