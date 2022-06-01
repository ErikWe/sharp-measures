namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Vectors.Parsing;

[Generator]
public class SharpMeasuresGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var globalAnalyzerConfig = GlobalAnalyzerConfigProvider.Attach(context.AnalyzerConfigOptionsProvider);
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider);

        var unitParsingResult = UnitParsingStage.Attach(context);
        var scalarParsingResult = ScalarParsingStage.Attach(context);
        var vectorParsingResult = VectorParsingStage.Attach(context);

        UnitGenerator.Attach(context, unitParsingResult.UnitProvider, unitParsingResult.UnitPopulationProvider,
            scalarParsingResult.ScalarPopulationProvider, globalAnalyzerConfig, documentationDictionary);

        ScalarQuantityGenerator.Attach(context, scalarParsingResult.ScalarProvider, unitParsingResult.UnitPopulationProvider,
            scalarParsingResult.ScalarPopulationProvider, vectorParsingResult.VectorPopulationProvider, globalAnalyzerConfig, documentationDictionary);
    }
}
