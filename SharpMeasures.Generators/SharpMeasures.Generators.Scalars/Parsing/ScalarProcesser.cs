namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public static class ScalarProcesser
{
    private static ScalarBaseProcesser ScalarBaseProcesser { get; } = new(ScalarProcessingDiagnosticsStrategies.Default);
    private static ScalarSpecializationProcesser ScalarSpecializationProcesser { get; } = new(ScalarProcessingDiagnosticsStrategies.Default);

    public static (ScalarProcessingResult ProcessingResult, IncrementalValueProvider<IScalarPopulation> Population) Process(IncrementalGeneratorInitializationContext context, ScalarParsingResult parsingResult)
    {
        var scalarBases = parsingResult.ScalarBaseProvider.Select(ScalarBaseProcesser.Process).ReportDiagnostics(context);
        var scalarSpecializations = parsingResult.ScalarSpecializationProvider.Select(ScalarSpecializationProcesser.Process).ReportDiagnostics(context);

        var scalarBaseInterfaces = scalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = scalarSpecializations.Select(ExtractInterface).CollectResults();

        var populationAndProcessingData = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        var population = populationAndProcessingData.Select(ExtractPopulation);
        var processingData = populationAndProcessingData.Select(ExtractProcessingData);

        return (new ScalarProcessingResult(scalarBases, scalarSpecializations, processingData), population);
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation ExtractPopulation<T>((IScalarPopulation Population, T) input, CancellationToken _) => input.Population;
    private static ScalarProcessingData ExtractProcessingData<T>((T, ScalarProcessingData ProcessingData) input, CancellationToken _) => input.ProcessingData;

    private static (IScalarPopulation Population, ScalarProcessingData ProcessingData) CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _) => ScalarPopulation.Build(scalars.Bases, scalars.Specializations);
}
