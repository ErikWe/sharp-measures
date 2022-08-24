namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class VectorMathsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorDataModel> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(VectorDataModel model, CancellationToken _)
    {
        var unit = model.UnitPopulation.Units[model.Vector.Unit];

        return new(model.Vector.Type, model.Vector.Dimension, model.Vector.ImplementSum, model.Vector.ImplementDifference, model.Vector.Difference, model.Vector.Scalar, GetSquaredScalar(model),
            model.Vector.Unit, unit.Definition.Quantity, model.Documentation);
    }

    private static NamedType? GetSquaredScalar(VectorDataModel model)
    {
        if (model.Vector.Scalar is null)
        {
            return null;
        }

        return model.ScalarPopulation.Scalars[model.Vector.Scalar.Value].Square;
    }
}
