namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> provider)
    {
        IncrementalValuesProvider<Stage4.Result> fourthStage = Stage4.Attach(provider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
