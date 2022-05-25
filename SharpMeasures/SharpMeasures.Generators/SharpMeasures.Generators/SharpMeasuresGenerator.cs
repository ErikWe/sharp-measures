namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.AnalyzerConfig;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Parsing.Units;
using SharpMeasures.Generators.Parsing.Vectors;
using SharpMeasures.Generators.Units;

using System.Threading;

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

        var population = unitParsingResult.UnitPopulationProvider.Combine(scalarParsingResult.ScalarPopulationProvider)
            .Combine(vectorParsingResult.VectorPopulationProvider).Select(CombinePopulations);

        UnitGenerator.Attach(context, unitParsingResult.ParsedUnitProvider, scalarParsingResult.ScalarPopulationProvider, globalAnalyzerConfig, documentationDictionary);
    }

    private static SharpMeasuresPopulation CombinePopulations
        (((NamedTypePopulation<UnitInterface>, NamedTypePopulation<ScalarInterface>), NamedTypePopulation<VectorInterface>) populations, CancellationToken _)
    {
        return new(populations.Item1.Item1, populations.Item1.Item2, populations.Item2);
    }
}
