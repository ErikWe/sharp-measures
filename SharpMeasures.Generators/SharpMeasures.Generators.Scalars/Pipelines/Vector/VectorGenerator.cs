namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel model, CancellationToken _)
    {
        if (model.Scalar.Vector is null)
        {
            return null;
        }

        if (model.VectorPopulation.Vectors.TryGetValue(model.Scalar.Vector.Value, out var vector))
        {
            return new(model.Scalar.Type, model.Scalar.Vector.Value, vector.Dimension, model.Documentation);
        }

        if (model.VectorPopulation.Groups.TryGetValue(model.Scalar.Vector.Value, out var group))
        {
            var dimensions = group.MembersByDimension.Keys;

            return new(model.Scalar.Type, model.Scalar.Vector.Value, dimensions.ToList(), model.Documentation);
        }

        return null;
    }
}
