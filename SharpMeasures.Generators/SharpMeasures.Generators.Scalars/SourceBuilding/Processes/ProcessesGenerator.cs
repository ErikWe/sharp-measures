namespace SharpMeasures.Generators.Scalars.SourceBuilding.Processes;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
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

        List<IQuantityProcess> processes = new(model.Value.Scalar.Processes.Count + model.Value.Scalar.InheritedProcesses.Count);

        foreach (var process in model.Value.Scalar.Processes)
        {
            processes.Add(process);
        }

        foreach (var process in model.Value.Scalar.InheritedProcesses)
        {
            processes.Add(process);
        }

        return new DataModel(model.Value.Scalar.Type, processes, model.Value.SourceBuildingContext);
    }
}
