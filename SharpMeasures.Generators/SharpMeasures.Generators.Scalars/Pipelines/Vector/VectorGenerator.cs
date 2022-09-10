﻿namespace SharpMeasures.Generators.Scalars.Pipelines.Vectors;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Scalar.Vector is null)
        {
            return new Optional<DataModel>();
        }

        if (model.Value.VectorPopulation.Vectors.TryGetValue(model.Value.Scalar.Vector.Value, out var vector))
        {
            return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Vector.Value, vector.Dimension, model.Value.Documentation);
        }

        if (model.Value.VectorPopulation.Groups.TryGetValue(model.Value.Scalar.Vector.Value, out var group))
        {
            var dimensions = group.MembersByDimension.Keys;

            return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Vector.Value, dimensions.ToList(), model.Value.Documentation);
        }

        return new Optional<DataModel>();
    }
}
