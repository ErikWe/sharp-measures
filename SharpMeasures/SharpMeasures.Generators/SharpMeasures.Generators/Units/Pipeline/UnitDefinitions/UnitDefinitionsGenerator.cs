namespace SharpMeasures.Generators.Units.Pipeline.UnitDefinitions;

using Microsoft.CodeAnalysis;

internal static class UnitDefinitionsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var withDefinitions = DefinitionsStage.ExtractDefinitions(context, inputProvider);

        context.RegisterSourceOutput(withDefinitions, Execution.Execute);
    }
}
