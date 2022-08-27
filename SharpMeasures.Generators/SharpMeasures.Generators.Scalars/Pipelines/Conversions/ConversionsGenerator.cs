namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> modelProvider)
    {
        var filteredAndReduced = modelProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel model, CancellationToken _)
    {
        if (model.Scalar.Conversions.Count is 0)
        {
            return null;
        }

        return new(model.Scalar.Type, model.Scalar.Conversions, model.Documentation);
    }
}
