namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Collections.Immutable;
using System.Threading;

public interface IScalarResolver
{
    public abstract (IncrementalValueProvider<IResolvedScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider);
}

internal class ScalarResolver : IScalarResolver
{
    private IncrementalValueProvider<IScalarPopulationWithData> ScalarPopulationProvider { get; }

    private IncrementalValuesProvider<ScalarBaseType> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<ScalarSpecializationType> ScalarSpecializationProvider { get; }

    internal ScalarResolver(IncrementalValueProvider<IScalarPopulationWithData> scalarPopulationProvider, IncrementalValuesProvider<ScalarBaseType> scalarBaseProvider,
        IncrementalValuesProvider<ScalarSpecializationType> scalarSpecializationProvider)
    {
        ScalarPopulationProvider = scalarPopulationProvider;

        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IResolvedScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IVectorPopulation> vectorPopulationProvider)
    {
        var resolvedScalarBases = ScalarBaseResolver.Resolve(ScalarBaseProvider, unitPopulationProvider);
        var resolvedScalarSpecializations = ScalarSpecializationResolver.Resolve(ScalarSpecializationProvider, unitPopulationProvider, ScalarPopulationProvider);

        var scalarBaseInterfaces = resolvedScalarBases.Select(ExtractInterface).Collect();
        var scalarSpecializationInterfaces = resolvedScalarSpecializations.Select(ExtractInterface).Collect();

        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (population, new ScalarGenerator(population, resolvedScalarBases, resolvedScalarSpecializations));
    }

    private static IResolvedScalarType ExtractInterface(ResolvedScalarType scalarType, CancellationToken _) => scalarType;

    private static IResolvedScalarPopulation CreatePopulation((ImmutableArray<IResolvedScalarType> Bases, ImmutableArray<IResolvedScalarType> Specializations) scalars, CancellationToken _)
    {
        return ResolvedScalarPopulation.Build(scalars.Bases, scalars.Specializations);
    }
}
