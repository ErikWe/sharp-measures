namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorUnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorDataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(VectorDataModel model, CancellationToken _)
    {
        if (model.Vector.IncludedUnits.Count is 0 && model.Vector.Constants.Count is 0)
        {
            return null;
        }

        var unit = model.UnitPopulation.Units[model.Vector.Unit];

        var includedUnits = model.Vector.IncludedUnits.Select((unitName) => unit.UnitsByName[unitName]).ToList();

        return new(model.Vector.Type, model.Vector.Dimension, model.Vector.Scalar, model.Vector.Unit, unit.Definition.Quantity, includedUnits, model.Vector.Constants, model.Documentation);
    }
}
