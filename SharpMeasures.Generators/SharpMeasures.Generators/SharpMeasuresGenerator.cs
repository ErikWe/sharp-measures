namespace SharpMeasures.Generators;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Providers.AnalyzerConfig;
using SharpMeasures.Generators.Scalars.Parsing;
using SharpMeasures.Generators.Units.Parsing;
using SharpMeasures.Generators.Vectors.Parsing;

[Generator]
public sealed class SharpMeasuresGenerator : IIncrementalGenerator
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

        (var foreignUnitProcesser, var foreignScalarProcesser, var foreignVectorProcesser) = ForeignTypeParser.Parse(foreignSymbols);

        (var extendedUnitPopulation, var foreignUnitValidator) = ForeignTypeProcesser.Process(foreignUnitProcesser, unvalidatedUnitPopulation);
        (var extendedScalarPopulation, var foreignScalarValidator) = ForeignTypeProcesser.Process(foreignScalarProcesser, unvalidatedScalarPopulation);
        (var extendedVectorPopulation, var foreignVectorValidator) = ForeignTypeProcesser.Process(foreignVectorProcesser, unvalidatedVectorPopulation);

        (var validatedUnitPopulation, var unitGenerator) = unitValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation);
        (var unresolvedScalarPopulation, var scalarResolver) = scalarValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);
        (var unresolvedVectorPopulation, var vectorResolver) = vectorValidator.Validate(context, extendedUnitPopulation, extendedScalarPopulation, extendedVectorPopulation);

        var extendedValidatedUnitPopulation = ForeignTypeValidator.Validate(foreignUnitValidator, unvalidatedUnitPopulation, unvalidatedScalarPopulation, validatedUnitPopulation);
        (var extendedUnresolvedScalarPopulation, var foreignScalarResolver) = ForeignTypeValidator.Validate(foreignScalarValidator, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation, unresolvedScalarPopulation);
        (var extendedUnresolvedVectorPopulation, var foreignVectorResolver) = ForeignTypeValidator.Validate(foreignVectorValidator, unvalidatedUnitPopulation, unvalidatedScalarPopulation, unvalidatedVectorPopulation, unresolvedVectorPopulation);

        (var resolvedScalarPopulation, var scalarGenerator) = scalarResolver.Resolve(context, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, extendedUnresolvedVectorPopulation);
        (var resolvedVectorPopulation, var vectorGenerator) = vectorResolver.Resolve(context, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, extendedUnresolvedVectorPopulation);

        var extendedResolvedScalarPopulation = ForeignTypeResolver.Resolve(foreignScalarResolver, extendedValidatedUnitPopulation, extendedUnresolvedScalarPopulation, resolvedScalarPopulation);
        var extendedResolvedVectorPopulation = ForeignTypeResolver.Resolve(foreignVectorResolver, extendedValidatedUnitPopulation, extendedUnresolvedVectorPopulation, resolvedVectorPopulation);

        unitGenerator.Generate(context, extendedValidatedUnitPopulation, resolvedScalarPopulation, globalAnalyzerConfig, documentationDictionary);
        scalarGenerator.Generate(context, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
        vectorGenerator.Generate(context, extendedValidatedUnitPopulation, extendedResolvedScalarPopulation, extendedResolvedVectorPopulation, globalAnalyzerConfig, documentationDictionary);
    }
}
