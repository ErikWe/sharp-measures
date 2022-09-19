namespace SharpMeasures.Generators.Scalars.Pipelines.Units;

using Microsoft.CodeAnalysis;

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
        if (model.HasValue is false || model.Value.Scalar.IncludedUnitBaseInstanceNames.Count is 0 && model.Value.Scalar.IncludedUnitInstanceNames.Count is 0 && model.Value.Scalar.Constants.Count is 0)
        {
            return new Optional<DataModel>();
        }

        var unit = model.Value.UnitPopulation.Units[model.Value.Scalar.Unit];

        var includedUnitBaseInstances = model.Value.Scalar.IncludedUnitBaseInstanceNames.Select((unitName) => unit.UnitInstancesByName[unitName]).ToList();
        var includedUnitInstances = model.Value.Scalar.IncludedUnitInstanceNames.Select((unitName) => unit.UnitInstancesByName[unitName]).ToList();

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Unit, unit.Definition.Quantity, includedUnitBaseInstances, includedUnitInstances, model.Value.Scalar.Constants, model.Value.Documentation);
    }
}
