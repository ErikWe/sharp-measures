namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<ParameterStage.Result> inputProvider)
    {
        var fourthStage = ReductionStage.ReduceData(inputProvider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
