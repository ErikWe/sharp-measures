namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Immutable;
using System.Threading;

public interface IScalarResolver
{
    public abstract (IncrementalValueProvider<IResolvedScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider);
}

internal sealed class ScalarResolver : IScalarResolver
{
    private IncrementalValuesProvider<Optional<ScalarBaseType>> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<Optional<ScalarSpecializationType>> ScalarSpecializationProvider { get; }

    internal ScalarResolver(IncrementalValuesProvider<Optional<ScalarBaseType>> scalarBaseProvider, IncrementalValuesProvider<Optional<ScalarSpecializationType>> scalarSpecializationProvider)
    {
        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IResolvedScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider, IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var resolvedScalarBases = ScalarBaseResolver.Resolve(ScalarBaseProvider, unitPopulationProvider);
        var resolvedScalarSpecializations = ScalarSpecializationResolver.Resolve(ScalarSpecializationProvider, unitPopulationProvider, scalarPopulationProvider);

        var scalarBaseInterfaces = resolvedScalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = resolvedScalarSpecializations.Select(ExtractInterface).CollectResults();

        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (population, new ScalarGenerator(resolvedScalarBases, resolvedScalarSpecializations));
    }

    private static Optional<IResolvedScalarType> ExtractInterface(Optional<ResolvedScalarType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IResolvedScalarType>();
    private static IResolvedScalarPopulation CreatePopulation((ImmutableArray<IResolvedScalarType> Bases, ImmutableArray<IResolvedScalarType> Specializations) scalars, CancellationToken _) => ResolvedScalarPopulation.Build(scalars.Bases, scalars.Specializations);
}
