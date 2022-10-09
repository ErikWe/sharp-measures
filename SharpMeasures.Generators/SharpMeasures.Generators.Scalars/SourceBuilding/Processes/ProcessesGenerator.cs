namespace SharpMeasures.Generators.Scalars.SourceBuilding.Processes;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ProcessesGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Optional<Scalars.DataModel>> modelProvider)
    {
        var reduced = modelProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static Optional<DataModel> Reduce(Optional<Scalars.DataModel> model, CancellationToken _)
    {
        if (model.HasValue is false || model.Value.Scalar.Processes.Count is 0 && model.Value.Scalar.InheritedProcesses.Count is 0)
        {
            return new Optional<DataModel>();
        }

        return new DataModel(model.Value.Scalar.Type, model.Value.Scalar.Processes.Concat(model.Value.Scalar.InheritedProcesses).ToList(), model.Value.SourceBuildingContext);
    }
}
