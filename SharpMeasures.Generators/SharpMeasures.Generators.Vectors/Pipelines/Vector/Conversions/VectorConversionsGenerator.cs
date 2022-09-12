namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Vector.Conversions.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Conversions, model.Value.VectorPopulation, model.Value.Documentation);
    }
}
