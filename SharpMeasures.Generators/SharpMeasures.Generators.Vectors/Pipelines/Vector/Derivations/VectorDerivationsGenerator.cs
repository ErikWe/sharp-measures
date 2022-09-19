namespace SharpMeasures.Generators.Vectors.Pipelines.Derivations;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorDerivationsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Vector.DefinedDerivations.Count is 0 && model.Value.Vector.InheritedDerivations.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Scalar is not null, model.Value.Vector.DefinedDerivations.Concat(model.Value.Vector.InheritedDerivations).ToList(), model.Value.ScalarPopulation, model.Value.Documentation);
    }
}
