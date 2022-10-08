namespace SharpMeasures.Generators.Scalars.Pipelines.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class CommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        if (model.Value.UnitPopulation.Units.TryGetValue(model.Value.Scalar.Unit, out var unit) is false)
        {
            return new Optional<DataModel>();
        }

        var unitParameterName = SourceBuildingUtility.ToParameterName(model.Value.Scalar.Unit.Name);

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Unit, unit.Definition.Quantity, unitParameterName, model.Value.Scalar.UseUnitBias, model.Value.Scalar.DefaultUnitInstanceName, model.Value.Scalar.DefaultUnitInstanceSymbol, model.Value.Documentation);
    }
}
