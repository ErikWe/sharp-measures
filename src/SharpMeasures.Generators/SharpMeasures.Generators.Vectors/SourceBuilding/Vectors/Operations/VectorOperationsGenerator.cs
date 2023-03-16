namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Operations;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
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
        if (model.HasValue is false || (model.Value.Vector.Operations.Count is 0 && model.Value.Vector.InheritedOperations.Count is 0 && model.Value.Vector.VectorOperations.Count is 0 && model.Value.Vector.InheritedVectorOperations.Count is 0))
        {
            return new Optional<DataModel>();
        }

        List<IQuantityOperation> operations = new(model.Value.Vector.Operations.Count + model.Value.Vector.InheritedOperations.Count);
        List<IVectorOperation> vectorOperations = new(model.Value.Vector.VectorOperations.Count + model.Value.Vector.InheritedVectorOperations.Count);

        foreach (var operation in model.Value.Vector.Operations)
        {
            operations.Add(operation);
        }

        foreach (var operation in model.Value.Vector.InheritedOperations)
        {
            operations.Add(operation);
        }

        foreach (var operation in model.Value.Vector.VectorOperations)
        {
            vectorOperations.Add(operation);
        }

        foreach (var operation in model.Value.Vector.InheritedVectorOperations)
        {
            vectorOperations.Add(operation);
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Dimension, operations, vectorOperations, model.Value.ScalarPopulation, model.Value.VectorPopulation, model.Value.SourceBuildingContext);
    }
}
