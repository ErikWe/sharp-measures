namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitionsPipeline;

using Microsoft.CodeAnalysis;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        IncrementalValuesProvider<DefinitionsStage.Result> fourthStage = DefinitionsStage.ExtractDefinitions(context, inputProvider);

        context.RegisterSourceOutput(fourthStage, Execution.Execute);
    }
}
