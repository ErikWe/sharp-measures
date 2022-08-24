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
        if (model.Scalar.IncludedBases.Count is 0 && model.Scalar.IncludedUnits.Count is 0 && model.Scalar.Constants.Count is 0)
        {
            return null;
        }

        var unit = model.UnitPopulation.Units[model.Scalar.Unit];

        var includedBases = model.Scalar.IncludedBases.Select((unitName) => unit.UnitsByName[unitName]).ToList();
        var includedUnits = model.Scalar.IncludedUnits.Select((unitName) => unit.UnitsByName[unitName]).ToList();

        return new(model.Scalar.Type, model.Scalar.Unit, unit.Definition.Quantity, includedBases, includedUnits, model.Scalar.Constants, model.Documentation);
    }
}
