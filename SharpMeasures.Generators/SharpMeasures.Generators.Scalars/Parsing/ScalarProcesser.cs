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

        var populationWithData = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new ScalarValidator(populationWithData, scalarBases, scalarSpecializations));
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation ReducePopulation(IScalarPopulationWithData scalarPopulation, CancellationToken _) => scalarPopulation;

    private static IScalarPopulationWithData CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return ScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }
}
