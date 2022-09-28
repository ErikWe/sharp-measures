﻿namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Maths;

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

        var unit = model.Value.UnitPopulation.Units[model.Value.Vector.Unit];

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.ImplementSum, model.Value.Vector.ImplementDifference, model.Value.Vector.Difference, model.Value.Vector.Scalar, model.Value.Vector.Unit, unit.Definition.Quantity, model.Value.Documentation);
    }
}