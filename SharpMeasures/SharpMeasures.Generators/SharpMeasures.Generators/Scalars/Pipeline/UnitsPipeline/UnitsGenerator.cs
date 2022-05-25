namespace SharpMeasures.Generators.Scalars.Pipeline.UnitsPipeline;

using Microsoft.CodeAnalysis;

internal static class UnitsGenerator
{
    public static void Initialize(IncrementalGeneratorInitializationContext context, IncrementalValuesProvider<DocumentationStage.Result> inputProvider)
    {
        var withParsed = ParsingStage.Parse(context, inputProvider);

        DiagnosticsStage.ProduceAndReportDiagnostics(context, withParsed);
        context.RegisterSourceOutput(withParsed, Execution.Execute);
    }
}
