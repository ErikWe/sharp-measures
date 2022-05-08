namespace SharpMeasures.Generators.Units.Pipeline.ComparablePipeline;

using Microsoft.CodeAnalysis;

internal static class ComparableGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> provider)
    {
        var fourthStage = Stage4.MinimizeData(provider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
