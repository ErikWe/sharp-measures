namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Maths;

using Microsoft.CodeAnalysis;

using System.Threading;

internal static class VectorMathsGenerator
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

        if (model.Value.UnitPopulation.Units.TryGetValue(model.Value.Vector.Unit, out var unit) is false)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.ImplementSum, model.Value.Vector.ImplementDifference, model.Value.Vector.Difference, GetDifferenceScalar(model.Value), model.Value.Vector.Scalar, model.Value.Vector.Unit, unit.Definition.Quantity, model.Value.Documentation);
    }

    private static NamedType? GetDifferenceScalar(VectorDataModel model)
    {
        if (model.Vector.Difference is null)
        {
            return null;
        }

        if (model.VectorPopulation.Vectors.TryGetValue(model.Vector.Difference.Value, out var differenceVector))
        {
            return differenceVector.Scalar;
        }

        if (model.VectorPopulation.Groups.TryGetValue(model.Vector.Difference.Value, out var differenceGroup))
        {
            return differenceGroup.Scalar;
        }

        return null;
    }
}
