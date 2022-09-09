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
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider, globalAnalyzerConfig, DocumentationDiagnostics.Instance);

        (var unvalidatedUnitPopulation, var unitValidator) = UnitParser.Attach(context);
        (var unvalidatedScalarPopulation, var scalarValidator) = ScalarParser.Attach(context);
        (var unvalidatedVectorPopulation, var vectorValidator) = VectorParser.Attach(context);

        (var validUnitPopulation, var unitGenerator) = unitValidator.Validate(context, unvalidatedScalarPopulation);
        (var unresolvedScalarPopulation, var scalarResolver) = scalarValidator.Validate(context, unvalidatedUnitPopulation, unvalidatedVectorPopulation);
        (var unresolvedVectorPopulation, var vectorResolver) = vectorValidator.Validate(context, unvalidatedUnitPopulation, unvalidatedScalarPopulation);

        (var resolvedScalarPopulation, var scalarGenerator) = scalarResolver.Resolve(context, validUnitPopulation, unresolvedVectorPopulation);
        (var resolvedVectorPopulation, var vectorGenerator) = vectorResolver.Resolve(context, validUnitPopulation, unresolvedScalarPopulation);

        unitGenerator.Generate(context, resolvedScalarPopulation, globalAnalyzerConfig, documentationDictionary);
        scalarGenerator.Generate(context, validUnitPopulation, resolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
        vectorGenerator.Generate(context, validUnitPopulation, resolvedScalarPopulation, globalAnalyzerConfig, documentationDictionary);
    }
}
