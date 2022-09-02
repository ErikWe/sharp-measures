namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel model, CancellationToken _)
    {
        if (model.Scalar.IncludedUnitBaseInstancesNames.Count is 0 && model.Scalar.IncludedUnitInstanceNames.Count is 0 && model.Scalar.Constants.Count is 0)
        {
            return null;
        }

        var unit = model.UnitPopulation.Units[model.Scalar.Unit];

        var includedUnitBaseInstances = model.Scalar.IncludedUnitBaseInstancesNames.Select((unitName) => unit.UnitInstancesByName[unitName]).ToList();
        var includedUnitInstances = model.Scalar.IncludedUnitInstanceNames.Select((unitName) => unit.UnitInstancesByName[unitName]).ToList();

        return new(model.Scalar.Type, model.Scalar.Unit, unit.Definition.Quantity, includedUnitBaseInstances, includedUnitInstances, model.Scalar.Constants, model.Documentation);
    }
}
