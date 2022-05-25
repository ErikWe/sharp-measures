namespace SharpMeasures.Generators.Scalars.Pipeline.DimensionalEquivalencePipeline;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Utility;

using System.Threading;

internal static class DimensionalEquivalenceGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var reducedUnbiased = inputProvider.Select(Reduce);

        context.RegisterSourceOutput(reducedUnbiased, Execution.Execute);
    }
}
