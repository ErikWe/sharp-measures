namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class VectorConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<VectorDataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(VectorDataModel model, CancellationToken _)
    {
        if (model.Vector.Conversions.Count is 0)
        {
            return null;
        }

        return new(model.Vector.Type, model.Vector.Dimension, model.Vector.Conversions, model.VectorPopulation, model.Documentation);
    }
}
