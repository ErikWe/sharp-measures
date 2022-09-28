namespace SharpMeasures.Generators.Vectors.Pipelines.Vectors.Processes;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorProcessesGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<VectorDataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<VectorDataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Vector.Processes.Count is 0 && model.Value.Vector.InheritedProcesses.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Vector.Type, model.Value.Vector.Processes.Concat(model.Value.Vector.InheritedProcesses).ToList(), model.Value.Documentation);
    }
}
