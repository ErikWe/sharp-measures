namespace SharpMeasures.Generators.Units.Pipeline.DefinitionsPipeline;

using Microsoft.CodeAnalysis;

internal static class DefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> provider)
    {
        IncrementalValuesProvider<Stage4.Result> fourthStage = Stage4.Perform(provider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
