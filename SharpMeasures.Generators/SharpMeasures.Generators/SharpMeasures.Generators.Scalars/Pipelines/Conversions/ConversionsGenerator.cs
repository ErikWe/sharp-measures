namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using Microsoft.CodeAnalysis;

using System.Linq;
using System.Threading;

internal static class ConversionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Scalars.DataModel> inputProvider)
    {
        var reduced = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reduced, Execution.Execute);
    }

    private static DataModel Reduce(Scalars.DataModel input, CancellationToken _)
    {
        return new(input.Scalar.Type, input.Scalar.Conversions, input.Documentation);
    }
}
