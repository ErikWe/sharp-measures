namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.AnalyzerConfig;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing;

[Generator]
public sealed class SharpMeasuresGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var globalAnalyzerConfig = GlobalAnalyzerConfigProvider.Attach(context.AnalyzerConfigOptionsProvider);
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider, globalAnalyzerConfig, DocumentationDiagnostics.Instance);

        (var unitParsingResult, var unitForeignSymbols) = UnitParser.Attach(context);
        (var scalarParsingResult, var scalarForeignSymbols) = ScalarParser.Attach(context);
        (var vectorParsingResult, var vectorForeignSymbols) = VectorParser.Attach(context);

        (var unitProcessingResult, var unvalidatedUnitPopulation) = UnitProcesser.Process(context, unitParsingResult);
        (var scalarProcessingResult, var unvalidatedScalarPopulation) = ScalarProcesser.Process(context, scalarParsingResult);
        (var vectorProcessingResult, var unvalidatedVectorPopulation) = VectorProcesser.Process(context, vectorParsingResult);

        var foreignSymbols = unitForeignSymbols.Concat(scalarForeignSymbols).Concat(vectorForeignSymbols);

        (var foreignUnitParsingResult, var foreignScalarParsingResult, var foreignVectorProcesser) = ForeignTypeParser.Parse(foreignSymbols);

        (var foreignUnitProcessingResult, var extendedUnitPopulation) = ForeignTypeProcesser.Process(foreignUnitParsingResult, unvalidatedUnitPopulation);
        (var foreignScalarProcessingResult, var extendedScalarPopulation) = ForeignTypeProcesser.Process(foreignScalarParsingResult, unvalidatedScalarPopulation);
        (var foreignVectorProcessingResult, var extendedVectorPopulation) = ForeignTypeProcesser.Process(foreignVectorProcesser, unvalidatedVectorPopulation);

        (var unitValidationResult, var validatedUnitPopulation) = UnitValidator.Validate(context, unitProcessingResult, extendedUnitPopulation, extendedScalarPopulation);
        (var scalarValidationResult, var unresolvedScalarPopulation) = ScalarValidator.Validate(context, scalarProcessingResult, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
        (var vectorValidationResult, var unresolvedVectorPopulation) = VectorValidator.Validate(context, vectorProcessingResult, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);

        var extendedValidatedUnitPopulation = ForeignTypeValidator.Validate(foreignUnitProcessingResult, unvalidatedUnitPopulation, unvalidatedScalarPopulation, validatedUnitPopulation);
        (var foreignScalarValidationResult, var extendedUnresolvedScalarPopulation) = ForeignTypeValidator.Validate(foreignScalarProcessingResult, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation, unresolvedScalarPopulation);
        (var foreignVectorValidationResult, var extendedUnresolvedVectorPopulation) = ForeignTypeValidator.Validate(foreignVectorProcessingResult, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation, unresolvedVectorPopulation);

        (var scalarResolutionResult, var resolvedScalarPopulation) = ScalarResolver.Resolve(scalarValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation);
        (var vectorResulutionResult, var resolvedVectorPopulation) = VectorResolver.Resolve(vectorValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedVectorPopulation);

        var extendedResolvedScalarPopulation = ForeignTypeResolver.Resolve(foreignScalarValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, resolvedScalarPopulation);
        var extendedResolvedVectorPopulation = ForeignTypeResolver.Resolve(foreignVectorValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedVectorPopulation, resolvedVectorPopulation);

        UnitGenerator.Generate(context, unitValidationResult, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, globalAnalyzerConfig, documentationDictionary);
        ScalarGenerator.Generate(context, scalarResolutionResult, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
        VectorGenerator.Generate(context, vectorResulutionResult, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
    }
}
