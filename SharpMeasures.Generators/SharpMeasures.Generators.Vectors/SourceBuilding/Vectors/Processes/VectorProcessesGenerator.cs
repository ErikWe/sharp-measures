namespace SharpMeasures.Generators.Vectors.SourceBuilding.Vectors.Processes;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
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

        List<IQuantityProcess> processes = new(model.Value.Vector.Processes.Count + model.Value.Vector.InheritedProcesses.Count);

        foreach (var process in model.Value.Vector.Processes)
        {
            processes.Add(process);
        }

        foreach (var process in model.Value.Vector.InheritedProcesses)
        {
            processes.Add(process);
        }

        return new DataModel(model.Value.Vector.Type, processes, model.Value.SourceBuildingContext);
    }
}
