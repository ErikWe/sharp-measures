namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IScalarProcesser
{
    public abstract (IncrementalValueProvider<IScalarPopulation> Population, IScalarValidator Validator) Process(IncrementalGeneratorInitializationContext context);
}

internal class ScalarProcesser : IScalarProcesser
{
    private IncrementalValuesProvider<Optional<RawScalarBaseType>> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<Optional<RawScalarSpecializationType>> ScalarSpecializationProvider { get; }

    public ScalarProcesser(IncrementalValuesProvider<Optional<RawScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<RawScalarSpecializationType>> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IScalarPopulation>, IScalarValidator) Process(IncrementalGeneratorInitializationContext context)
    {
        ScalarBaseProcesser scalarBaseProcesser = new();
        ScalarSpecializationProcesser scalarSpecializationProcesser = new();

        var scalarBases = ScalarBaseProvider.Select(scalarBaseProcesser.Process).ReportDiagnostics(context);
        var scalarSpecializations = ScalarSpecializationProvider.Select(scalarSpecializationProcesser.Process).ReportDiagnostics(context);

        var scalarBaseInterfaces = scalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = scalarSpecializations.Select(ExtractInterface).CollectResults();

        var populationAndProcessingData = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        var population = populationAndProcessingData.Select(ExtractPopulation);
        var processingData = populationAndProcessingData.Select(ExtractProcessingData);

        return (population, new ScalarValidator(processingData, scalarBases, scalarSpecializations));
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation ExtractPopulation((IScalarPopulation Population, ScalarProcessingData) input, CancellationToken _) => input.Population;
    private static ScalarProcessingData ExtractProcessingData((IScalarPopulation, ScalarProcessingData ProcessingData) input, CancellationToken _) => input.ProcessingData;

    private static (IScalarPopulation Population, ScalarProcessingData ProcessingData) CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return ScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }
}
