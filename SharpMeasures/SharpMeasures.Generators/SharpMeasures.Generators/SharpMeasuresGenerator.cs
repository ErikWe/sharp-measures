namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.AnalyzerConfig;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Vectors.Parsing;

[Generator]
public class SharpMeasuresGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var globalAnalyzerConfig = GlobalAnalyzerConfigProvider.Attach(context.AnalyzerConfigOptionsProvider);
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider, DocumentationDiagnostics.Instance);

        var unitParsingResult = UnitParsingStage.Attach(context);
        var scalarParsingResult = ScalarParsingStage.Attach(context);
        var vectorParsingResult = VectorParsingStage.Attach(context);

        unitParsingResult.Perform(context, scalarParsingResult.PopulationProvider, globalAnalyzerConfig, documentationDictionary);
        scalarParsingResult.Perform(context, unitParsingResult.PopulationProvider, vectorParsingResult.PopulationProvider, globalAnalyzerConfig, documentationDictionary);
        vectorParsingResult.Perform(context, unitParsingResult.PopulationProvider, scalarParsingResult.PopulationProvider, globalAnalyzerConfig, documentationDictionary);
    }
}
