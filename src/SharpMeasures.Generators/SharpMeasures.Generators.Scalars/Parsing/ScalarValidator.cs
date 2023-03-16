namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Immutable;
using System.Threading;

public static class ScalarValidator
{
    private static ScalarBaseValidator ScalarBaseValidator { get; } = new(ScalarValidationDiagnosticsStrategies.Default);
    private static ScalarSpecializationValidator ScalarSpecializationValidator { get; } = new(ScalarValidationDiagnosticsStrategies.Default);

    public static (ScalarValidationResult ValidationResult, IncrementalValueProvider<IScalarPopulation> Population) Validate(IncrementalGeneratorInitializationContext context, ScalarProcessingResult processingResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var validatedScalarBases = processingResult.ScalarBaseProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ScalarBaseValidator.Validate).ReportDiagnostics(context);
        var validatedScalarSpecializations = processingResult.ScalarSpecializationProvider.Combine(processingResult.ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider).Select(ScalarSpecializationValidator.Validate).ReportDiagnostics(context);

        var scalarBaseInterfaces = validatedScalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = validatedScalarSpecializations.Select(ExtractInterface).CollectResults();

        var result = new ScalarValidationResult(validatedScalarBases, validatedScalarSpecializations);
        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (result, population);
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _) => ScalarPopulation.BuildWithoutProcessingData(scalars.Bases, scalars.Specializations);
}
