namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var filteredAndReduced = inputProvider.Select(Reduce).WhereNotNull();

        context.RegisterSourceOutput(filteredAndReduced, Execution.Execute);
    }

    private static DataModel? Reduce(Scalars.DataModel input, CancellationToken _)
    {
        if (input.Scalar.Conversions.Count is 0)
        {
            return null;
        }

        return new(input.Scalar.Type, input.Scalar.Conversions, input.Documentation);
    }
}
