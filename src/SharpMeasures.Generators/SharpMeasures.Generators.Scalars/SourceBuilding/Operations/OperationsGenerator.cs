namespace SharpMeasures.Generators.Scalars.SourceBuilding.Operations;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Threading;

internal static class OperationsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || (model.Value.Scalar.Operations.Count is 0 && model.Value.Scalar.InheritedOperations.Count is 0))
        {
            return new Optional<DataModel>();
        }

        List<IQuantityOperation> operations = new(model.Value.Scalar.Operations.Count + model.Value.Scalar.InheritedOperations.Count);

        foreach (var operation in model.Value.Scalar.Operations)
        {
            operations.Add(operation);
        }

        foreach (var operation in model.Value.Scalar.InheritedOperations)
        {
            operations.Add(operation);
        }

        return new DataModel(model.Value.Scalar.Type, operations, model.Value.ScalarPopulation, model.Value.VectorPopulation, model.Value.SourceBuildingContext);
    }
}
