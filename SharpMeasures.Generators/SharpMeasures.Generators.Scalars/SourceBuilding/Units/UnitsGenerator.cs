namespace SharpMeasures.Generators.Scalars.SourceBuilding.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Scalar.IncludedUnitBaseInstanceNames.Count is 0 && model.Value.Scalar.IncludedUnitInstanceNames.Count is 0 && model.Value.Scalar.Constants.Count is 0 && model.Value.Scalar.InheritedConstants.Count is 0)
        {
            return new Optional<DataModel>();
        }

        if (model.Value.UnitPopulation.Units.TryGetValue(model.Value.Scalar.Unit, out var unit) is false)
        {
            return new Optional<DataModel>();
        }

        var includedUnitBaseInstances = GetIncludedUnitInstances(unit, model.Value.Scalar.IncludedUnitBaseInstanceNames);
        var includedUnitInstances = GetIncludedUnitInstances(unit, model.Value.Scalar.IncludedUnitInstanceNames);

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Unit, unit.Definition.Quantity, includedUnitBaseInstances, includedUnitInstances, model.Value.Scalar.Constants.Concat(model.Value.Scalar.InheritedConstants).ToList(), model.Value.SourceBuildingContext);
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
