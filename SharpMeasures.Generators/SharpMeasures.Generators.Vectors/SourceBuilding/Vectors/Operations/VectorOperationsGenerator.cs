namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Operations;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorOperationsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Vector.Operations.Count is 0 && model.Value.Vector.InheritedOperations.Count is 0 && model.Value.Vector.VectorOperations.Count is 0 && model.Value.Vector.InheritedVectorOperations.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, model.Value.Vector.Operations.Concat(model.Value.Vector.InheritedOperations).ToList(), model.Value.Vector.VectorOperations.Concat(model.Value.Vector.InheritedVectorOperations).ToList(), model.Value.ScalarPopulation, model.Value.VectorPopulation, model.Value.SourceBuildingContext);
    }
}
