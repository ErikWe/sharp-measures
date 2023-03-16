namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Configuration;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Vectors;
using SharpMeasures.Generators.Vectors.Parsing;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

[Generator]
public sealed class SharpMeasuresGenerator : IIncrementalGenerator
{
    private static IEnumerable<Type> TargetAttributes { get; } = Units.TargetAttributes.Attributes.Concat(Scalars.TargetAttributes.Attributes).Concat(Vectors.TargetAttributes.Attributes);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var globalAnalyzerConfig = GlobalAnalyzerConfigProvider.Attach(context.AnalyzerConfigOptionsProvider);
        var documentationDictionary = DocumentationDictionaryProvider.AttachAndReport(context, context.AdditionalTextsProvider, globalAnalyzerConfig, DocumentationDiagnostics.Instance);

        var markedTypeDeclarationConfiguration = globalAnalyzerConfig.Select(ConstructMarkedTypeDeclarationConfiguration);
        var targetTypes = MarkedTypeDeclarationProvider.ConstructTargeted().RegisterConfigurationProvider(markedTypeDeclarationConfiguration).AttachAnyOf(context.SyntaxProvider, TargetAttributes);

        (var unitParsingResult, var unitForeignSymbols) = UnitParser.Attach(context, targetTypes);
        (var scalarParsingResult, var scalarForeignSymbols) = ScalarParser.Attach(context, targetTypes);
        (var vectorParsingResult, var vectorForeignSymbols) = VectorParser.Attach(context, targetTypes);

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

        var extendedValidatedUnitPopulation = ForeignTypeValidator.Validate(foreignUnitProcessingResult, extendedUnitPopulation, extendedScalarPopulation, validatedUnitPopulation);
        (var foreignScalarValidationResult, var extendedUnresolvedScalarPopulation) = ForeignTypeValidator.Validate(foreignScalarProcessingResult, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation, unresolvedScalarPopulation);
        (var foreignVectorValidationResult, var extendedUnresolvedVectorPopulation) = ForeignTypeValidator.Validate(foreignVectorProcessingResult, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation, unresolvedVectorPopulation);

        (var scalarResolutionResult, var resolvedScalarPopulation) = ScalarResolver.Resolve(scalarValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation);
        (var vectorResulutionResult, var resolvedVectorPopulation) = VectorResolver.Resolve(vectorValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedVectorPopulation);

        var extendedResolvedScalarPopulation = ForeignTypeResolver.Resolve(foreignScalarValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, resolvedScalarPopulation);
        var extendedResolvedVectorPopulation = ForeignTypeResolver.Resolve(foreignVectorValidationResult, extendedValidatedUnitPopulation, extendedUnresolvedVectorPopulation, resolvedVectorPopulation);

        UnitGenerator.Generate(context, unitValidationResult, extendedValidatedUnitPopulation, documentationDictionary, globalAnalyzerConfig);
        ScalarGenerator.Generate(context, scalarResolutionResult, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, documentationDictionary, globalAnalyzerConfig);
        VectorGenerator.Generate(context, vectorResulutionResult, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, documentationDictionary, globalAnalyzerConfig);
    }

    private static MarkedTypeDeclarationConfiguration ConstructMarkedTypeDeclarationConfiguration(GlobalAnalyzerConfig globalConfiguration, CancellationToken _)
    {
        return new MarkedTypeDeclarationConfiguration(AllowAliases: globalConfiguration.AllowAttributeAliases);
    }
}
