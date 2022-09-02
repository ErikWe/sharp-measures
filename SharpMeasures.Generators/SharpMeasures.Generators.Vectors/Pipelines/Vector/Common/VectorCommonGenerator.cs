namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Common;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.SourceBuilding;

using System.Threading;

internal static class VectorCommonGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorDataModel> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(VectorDataModel model, CancellationToken _)
    {
        var unit = model.UnitPopulation.Units[model.Vector.Unit];

        string unitParameterName = SourceBuildingUtility.ToParameterName(model.Vector.Unit.Name);

        return new(model.Vector.Type, model.Vector.Dimension, model.Vector.Scalar, GetSquaredScalar(model), model.Vector.Unit, unit.Definition.Quantity, unitParameterName, model.Vector.DefaultUnitInstanceName,
            model.Vector.DefaultUnitInstanceSymbol, model.Documentation);
    }

    private static NamedType? GetSquaredScalar(VectorDataModel model)
    {
        if (model.Vector.Scalar is not null && model.ScalarPopulation.Scalars.TryGetValue(model.Vector.Scalar.Value, out var scalar))
        {
            return scalar.Square;
        }

        return null;
    }
}
