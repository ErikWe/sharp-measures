namespace SharpMeasures.Generators.Scalars.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Scalars.Parsing.Abstraction;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IScalarResolver
{
    public abstract (IncrementalValueProvider<IScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnresolvedUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IUnresolvedVectorPopulation> vectorPopulationProvider);
}

internal class ScalarResolver : IScalarResolver
{
    private IncrementalValueProvider<IUnresolvedScalarPopulationWithData> ScalarPopulationProvider { get; }

    private IncrementalValuesProvider<UnresolvedScalarBaseType> ScalarBaseProvider { get; }
    private IncrementalValuesProvider<UnresolvedScalarSpecializationType> ScalarSpecializationProvider { get; }

    internal ScalarResolver(IncrementalValueProvider<IUnresolvedScalarPopulationWithData> scalarPopulationProvider, IncrementalValuesProvider<UnresolvedScalarBaseType> scalarBaseProvider, IncrementalValuesProvider<UnresolvedScalarSpecializationType> scalarSpecializationProvider)
    {
        ScalarPopulationProvider = scalarPopulationProvider;

        ScalarBaseProvider = scalarBaseProvider;
        ScalarSpecializationProvider = scalarSpecializationProvider;
    }

    public (IncrementalValueProvider<IScalarPopulation>, IScalarGenerator) Resolve(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnresolvedUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IUnresolvedVectorPopulation> vectorPopulationProvider)
    {
        var resolvedScalarBases = ScalarBaseProvider.Combine(unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider).Select(ScalarBaseTypeResolution.Resolve).ReportDiagnostics(context);

        var resolvedScalarSpecializations = ScalarSpecializationProvider.Combine(unitPopulationProvider, ScalarPopulationProvider, vectorPopulationProvider)
            .Select(ScalarSpecializationTypeResolution.Resolve).ReportDiagnostics(context);

        var scalarBaseInterfaces = resolvedScalarBases.Select(ExtractInterface).Collect(); var scalarSpecializationInterfaces = resolvedScalarSpecializations.Select(ExtractInterface).Collect();

        var intermediatePopulation = scalarBaseInterfaces.Combine(scalarSpecializationInterfaces).Select(CreateIntermediatePopulation);

        var reducedScalarSpecializations = resolvedScalarSpecializations.Combine(intermediatePopulation).Select(ScalarSpecializationTypeResolution.Reduce) .ReportDiagnostics(context);

        var reducedScalarSpecializationInterfaces = reducedScalarSpecializations.Select(ExtractInterface).Collect();
        
        var population = scalarBaseInterfaces.Combine(reducedScalarSpecializationInterfaces).Select(CreatePopulation);

        return (population, new ScalarGenerator(resolvedScalarBases, reducedScalarSpecializations));
    }

    private static IScalarType ExtractInterface(IScalarType scalarType, CancellationToken _) => scalarType;
    private static IIntermediateScalarSpecializationType ExtractInterface(IIntermediateScalarSpecializationType scalarSpecializationType, CancellationToken _) => scalarSpecializationType;

    private static IIntermediateScalarPopulation CreateIntermediatePopulation ((ImmutableArray<IScalarType> Bases, ImmutableArray<IIntermediateScalarSpecializationType> Specializations) scalars, CancellationToken _)
    {
        return new IntermediateScalarPopulation(scalars.Bases.ToDictionary(static (scalar) => scalar.Type.AsNamedType()), scalars.Specializations.ToDictionary(static (scalar) => scalar.Type.AsNamedType()));
    }

    private static IScalarPopulation CreatePopulation((ImmutableArray<IScalarType> Bases, ImmutableArray<IScalarType> Specializations) scalars, CancellationToken _)
    {
        return new ScalarPopulation(scalars.Bases.Concat(scalars.Specializations).ToDictionary(static (scalar) => scalar.Type.AsNamedType()));
    }
}
