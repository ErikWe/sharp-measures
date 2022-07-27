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
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider, globalAnalyzerConfig,
            DocumentationDiagnostics.Instance);

        (var unresolvedUnitPopulation, var unitResolver) = UnitParser.Attach(context);
        (var unresolvedScalarPopulation, var scalarResolver) = ScalarParser.Attach(context);
        (var unresolvedVectorPopulation, var vectorResolver) = VectorParser.Attach(context);

        (var unitPopulation, var unitGenerator) = unitResolver.Resolve(context, unresolvedUnitPopulation, unresolvedScalarPopulation);
        (var scalarPopulation, var scalarGenerator) = scalarResolver.Resolve(context, unresolvedUnitPopulation, unresolvedScalarPopulation, unresolvedVectorPopulation);
        (var vectorPopulation, var vectorGenerator) = vectorResolver.Resolve(context, unresolvedUnitPopulation, unresolvedScalarPopulation, unresolvedVectorPopulation);

        unitGenerator.Perform(context, unitPopulation, scalarPopulation, globalAnalyzerConfig, documentationDictionary);
        scalarGenerator.Perform(context, unitPopulation, scalarPopulation, vectorPopulation, globalAnalyzerConfig, documentationDictionary);
        vectorGenerator.Perform(context, unitPopulation, scalarPopulation, vectorPopulation, globalAnalyzerConfig, documentationDictionary);
    }
}
