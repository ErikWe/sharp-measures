namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Immutable;
using System.Threading;

public interface IScalarValidator
{
    public abstract (IncrementalValueProvider<IScalarPopulation>, IScalarResolver) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider);
}

internal sealed class ScalarValidator : IScalarValidator
{
    private IncrementalValueProvider<ScalarProcessingData> ProcessingDataProvider { get; }

    private IncrementalValuesProvider<Optional<ScalarBaseType>> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal ScalarValidator(IncrementalValueProvider<ScalarProcessingData> processingDataProvider, IncrementalValuesProvider<Optional<ScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarSpecializationProvider)
    {
        ProcessingDataProvider = processingDataProvider;

        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IScalarPopulation>, IScalarResolver) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var validatedScalarBases = ScalarBaseValidator.Validate(context, ScalarBaseProvider, ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider);
        var validatedScalarSpecializations = ScalarSpecializationValidator.Validate(context, ScalarSpecializationProvider, ProcessingDataProvider, unitPopulationProvider, scalarPopulationProvider, vectorPopulationProvider);

        var scalarBaseInterfaces = validatedScalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = validatedScalarSpecializations.Select(ExtractInterface).CollectResults();

        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (population, new ScalarResolver(validatedScalarBases, validatedScalarSpecializations));
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _) => ScalarPopulation.BuildWithoutProcessingData(scalars.Bases, scalars.Specializations);
}
