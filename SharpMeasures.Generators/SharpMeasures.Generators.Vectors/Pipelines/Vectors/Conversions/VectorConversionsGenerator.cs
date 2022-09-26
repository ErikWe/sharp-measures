namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Conversions;

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
        if (model.HasValue is false || model.Value.Vector.Conversions.Count is 0 && model.Value.Vector.Group is null && model.Value.Vector.OriginalQuantity is null)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Group, model.Value.Vector.Conversions, model.Value.Vector.InheritedConversions, model.Value.Vector.SpecializationForwardsConversionBehaviour, model.Value.Vector.SpecializationBackwardsConversionBehaviour, model.Value.VectorPopulation, model.Value.Documentation);
    }
}
