namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Immutable;
using System.Threading;

public static class ScalarResolver
{
    public static (ScalarResolutionResult ResolutionResult, IncrementalValueProvider<IResolvedScalarPopulation> Population) Resolve(ScalarValidationResult validationResult, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        var resolvedScalarBases = ScalarBaseResolver.Resolve(validationResult.ScalarBaseProcider, unitPopulationProvider);
        var resolvedScalarSpecializations = ScalarSpecializationResolver.Resolve(validationResult.ScalarSpecializationProvider, unitPopulationProvider, scalarPopulationProvider);

        var scalarBaseInterfaces = resolvedScalarBases.Select(ExtractInterface).CollectResults();
        var scalarSpecializationInterfaces = resolvedScalarSpecializations.Select(ExtractInterface).CollectResults();

        var population = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreatePopulation);

        return (new ScalarResolutionResult(resolvedScalarBases, resolvedScalarSpecializations), population);
    }

    private static Optional<IResolvedScalarType> ExtractInterface(Optional<ResolvedScalarType> scalarType, CancellationToken _) => scalarType.HasValue ? scalarType.Value : new Optional<IResolvedScalarType>();
    private static IResolvedScalarPopulation CreatePopulation((ImmutableArray<IResolvedScalarType> Bases, ImmutableArray<IResolvedScalarType> Specializations) scalars, CancellationToken _) => ResolvedScalarPopulation.Build(scalars.Bases, scalars.Specializations);
}
