namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;
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
        if (model.HasValue is false || model.Value.Vector.IncludedUnitInstanceNames.Count is 0 && model.Value.Vector.Constants.Count is 0 && model.Value.Vector.InheritedConstants.Count is 0)
        {
            return new Optional<DataModel>();
        }

        if (model.Value.UnitPopulation.Units.TryGetValue(model.Value.Vector.Unit, out var unit) is false)
        {
            return new Optional<DataModel>();
        }

        var includedUnitInstances = GetIncludedUnitInstances(unit, model.Value.Vector.IncludedUnitInstanceNames);

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Scalar, model.Value.Vector.Unit, unit.Definition.Quantity, includedUnitInstances, model.Value.Vector.Constants.Concat(model.Value.Vector.InheritedConstants).ToList(), model.Value.Documentation);
    }

    private static List<IUnitInstance> GetIncludedUnitInstances(IUnitType unit, IReadOnlyList<string> includedUnitInstanceNames)
    {
        List<IUnitInstance> includedUnits = new();

        foreach (var includedUnitInstanceName in includedUnitInstanceNames)
        {
            if (unit.UnitInstancesByName.TryGetValue(includedUnitInstanceName, out var unitInstance))
            {
                includedUnits.Add(unitInstance);
            }
        }

        return includedUnits;
    }
}
