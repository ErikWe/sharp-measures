namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<Stage3.Result> provider)
    {
        IncrementalValuesProvider<Stage4.Result> fourthStage = Stage4.Attach(context, provider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
