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
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider);
}

internal class ScalarValidator : IScalarValidator
{
    private IncrementalValueProvider<IScalarPopulationWithData> ScalarPopulationProvider { get; }

    private IncrementalValuesProvider<Optional<ScalarBaseType>> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal ScalarValidator(IncrementalValueProvider<IScalarPopulationWithData> scalarPopulationProvider, IncrementalValuesProvider<Optional<ScalarBaseType>> scalarBaseProvider,
        IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarSpecializationProvider)
    {
        ScalarPopulationProvider = scalarPopulationProvider;

        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IScalarPopulation>, IScalarResolver) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var validatedScalarBases = ScalarBaseValidator.Validate(context, ScalarBaseProvider, unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider);
        var validatedScalarSpecializations = ScalarSpecializationValidator.Validate(context, ScalarSpecializationProvider, unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider);

        var scalarBaseInterfaces = validatedScalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = validatedScalarSpecializations.Select(ExtractInterface).CollectResults();

        var populationWithData = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new ScalarResolver(populationWithData, validatedScalarBases, validatedScalarSpecializations));
    }

    private static Optional<IScalarBaseType> ExtractInterface(Optional<ScalarBaseType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarBaseType>();
    private static Optional<IScalarSpecializationType> ExtractInterface(Optional<ScalarSpecializationType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IScalarSpecializationType>();

    private static IScalarPopulation ReducePopulation(IScalarPopulationWithData scalarPopulation, CancellationToken _) => scalarPopulation;

    private static IScalarPopulationWithData CreatePopulation((ImmutableArray<IScalarBaseType> Bases, ImmutableArray<IScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return ScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }
}
