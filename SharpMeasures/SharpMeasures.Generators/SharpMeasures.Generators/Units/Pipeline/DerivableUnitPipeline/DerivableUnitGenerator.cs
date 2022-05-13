namespace SharpMeasures.Generators.Units.Pipeline.DerivableUnitPipeline;

using Microsoft.CodeAnalysis;

internal static class DerivableUnitGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var withDefinitions = DerivableStage.ExtractDerivableDefinitions(context, inputProvider);
        var filtered = FilterStage.FilterDuplicates(context, withDefinitions);

        context.RegisterSourceOutput(withDefinitions, Execution.Execute);
    }
}
