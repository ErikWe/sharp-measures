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

        (var unitProcesser, var unitForeignSymbols) = UnitParser.Attach(context);
        (var scalarProcesser, var scalarForeignSymbols) = ScalarParser.Attach(context);
        (var vectorProcceser, var vectorForeignSymbols) = VectorParser.Attach(context);

        (var unvalidatedUnitPopulation, var unitValidator) = unitProcesser.Process(context);
        (var unvalidatedScalarPopulation, var scalarValidator) = scalarProcesser.Process(context);
        (var unvalidatedVectorPopulation, var vectorValidator) = vectorProcceser.Process(context);

        var foreignSymbols = unitForeignSymbols.Concat(scalarForeignSymbols).Concat(vectorForeignSymbols);

        var foreignTypes = ForeignTypeParserAndProcesser.Parse(foreignSymbols, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation);

        (var extendedUnitPopulation, var extendedScalarPopulation, var extendedVectorPopulation) = ForeignTypeExtender.Extend(foreignTypes, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation);

        (var validatedUnitPopulation, var unitGenerator) = unitValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation);
        (var unresolvedScalarPopulation, var scalarResolver) = scalarValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
        (var unresolvedVectorPopulation, var vectorResolver) = vectorValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);

        (var extendedValidatedUnitPopulation, var extendedUnresolvedScalarPopulation, var extendedUnresolvedVectorPopulation) = ForeignTypeExtender.Extend(foreignTypes, validatedUnitPopulation, unresolvedScalarPopulation, unresolvedVectorPopulation);

        (var resolvedScalarPopulation, var scalarGenerator) = scalarResolver.Resolve(context, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, extendedUnresolvedVectorPopulation);
        (var resolvedVectorPopulation, var vectorGenerator) = vectorResolver.Resolve(context, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, extendedUnresolvedVectorPopulation);

        var resolvedForeignTypes = ForeignTypeResolver.Resolve(foreignTypes, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, extendedUnresolvedVectorPopulation);

        (extendedValidatedUnitPopulation, var extendedResolvedScalarPopulation, var extendedResolvedVectorPopulation) = ForeignTypeExtender.Extend(resolvedForeignTypes, validatedUnitPopulation, resolvedScalarPopulation, resolvedVectorPopulation);

        unitGenerator.Generate(context, extendedValidatedUnitPopulation, resolvedScalarPopulation, globalAnalyzerConfig, documentationDictionary);
        scalarGenerator.Generate(context, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
        vectorGenerator.Generate(context, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
    }
}
