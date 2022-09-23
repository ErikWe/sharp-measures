namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class VectorCommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false)
        {
            return new Optional<DataModel>();
        }

        var unit = model.Value.UnitPopulation.Units[model.Value.Vector.Unit];

        string unitParameterName = SourceBuildingUtility.ToParameterName(model.Value.Vector.Unit.Name);

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Scalar, model.Value.Vector.Unit, unit.Definition.Quantity, unitParameterName, model.Value.Vector.DefaultUnitInstanceName, model.Value.Vector.DefaultUnitInstanceSymbol, model.Value.Documentation);
    }
}
