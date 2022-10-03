namespace SharpMeasures.Generators.Scalars.Pipelines.Operations;

using Microsoft.CodeAnalysis;

using System.Linq;
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
        if (model.HasValue is false || model.Value.Scalar.Operations.Count is 0 && model.Value.Scalar.InheritedOperations.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Operations.Concat(model.Value.Scalar.InheritedOperations).ToList(), model.Value.ScalarPopulation, model.Value.VectorPopulation, model.Value.Documentation);
    }
}
