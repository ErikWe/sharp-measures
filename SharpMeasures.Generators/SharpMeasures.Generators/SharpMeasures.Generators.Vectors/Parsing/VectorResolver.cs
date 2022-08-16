namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IVectorResolver
{
    public abstract (IncrementalValueProvider<IVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<IRawUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IRawScalarPopulation> scalarPopulationProvider);
}

internal class VectorResolver : IVectorResolver
{
    private IncrementalValueProvider<IUnresolvedVectorPopulationWithData> VectorPopulationProvider { get; }

    private IncrementalValuesProvider<UnresolvedVectorGroupBaseType> BaseVectorGroupProvider { get; }
    private IncrementalValuesProvider<UnresolvedVectorGroupSpecializationType> SpecializedVectorGroupProvider { get; }
    private IncrementalValuesProvider<UnresolvedVectorGroupMemberType> VectorGroupMemberProvider { get; }

    private IncrementalValuesProvider<UnresolvedIndividualVectorBaseType> BaseIndividualVectorProvider { get; }
    private IncrementalValuesProvider<UnresolvedIndividualVectorSpecializationType> SpecializedIndividualVectorProvider { get; }

    public VectorResolver(IncrementalValueProvider<IUnresolvedVectorPopulationWithData> vectorPopulationProvider, IncrementalValuesProvider<UnresolvedVectorGroupBaseType> baseVectorGroupProvider,
        IncrementalValuesProvider<UnresolvedVectorGroupSpecializationType> specializedVectorGroupProvider, IncrementalValuesProvider<UnresolvedVectorGroupMemberType> vectorGroupMemberProvider,
        IncrementalValuesProvider<UnresolvedIndividualVectorBaseType> baseIndividualVectorProvider, IncrementalValuesProvider<UnresolvedIndividualVectorSpecializationType> specializedIndividualVectorProvider)
    {
        VectorPopulationProvider = vectorPopulationProvider;

        BaseVectorGroupProvider = baseVectorGroupProvider;
        SpecializedVectorGroupProvider = specializedVectorGroupProvider;
        VectorGroupMemberProvider = vectorGroupMemberProvider;

        BaseIndividualVectorProvider = baseIndividualVectorProvider;
        SpecializedIndividualVectorProvider = specializedIndividualVectorProvider;
    }

    public (IncrementalValueProvider<IVectorPopulation>, IVectorGenerator) Resolve(IncrementalGeneratorInitializationContext context,
        IncrementalValueProvider<IRawUnitPopulation> unitPopulationProvider, IncrementalValueProvider<IRawScalarPopulation> scalarPopulationProvider)
    {
        var resolvedVectorGroupBases = BaseVectorGroupProvider.Combine(unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider)
            .Select(VectorGroupBaseTypeResolution.Resolve).ReportDiagnostics(context);

        var resolvedVectorGroupSpecializations = SpecializedVectorGroupProvider.Combine(unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider)
            .Select(VectorGroupSpecializationTypeResolution.Resolve).ReportDiagnostics(context);

        var resolvedVectorGroupMembers = VectorGroupMemberProvider.Combine(unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider)
            .Select(VectorGroupMemberResolution.Resolve).ReportDiagnostics(context);

        var resolvedIndividualVectorBases = BaseIndividualVectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider)
            .Select(IndividualVectorBaseTypeResolution.Resolve).ReportDiagnostics(context);

        var resolvedIndividualVectorSpecializations = SpecializedIndividualVectorProvider.Combine(unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider)
            .Select(IndividualVectorSpecializationTypeResolution.Resolve).ReportDiagnostics(context);

        var baseVectorGroupInterfaces = resolvedVectorGroupBases.Select(ExtractInterface).Collect();
        var specializedVectorGroupInterfaces = resolvedVectorGroupSpecializations.Select(ExtractInterface).Collect();
        var vectorGroupMemberInterfaces = resolvedVectorGroupMembers.Select(ExtractInterface).Collect();

        var baseIndividualVectorInterfaces = resolvedIndividualVectorBases.Select(ExtractInterface).Collect();
        var specializedIndividualVectorInterfaces = resolvedIndividualVectorSpecializations.Select(ExtractInterface).Collect();

        var intermediateGroupPopulation = baseVectorGroupInterfaces.Combine(specializedVectorGroupInterfaces, vectorGroupMemberInterfaces).Select(CreateIntermediatePopulation);
        var intermediateIndividualPopulation = baseIndividualVectorInterfaces.Combine(specializedIndividualVectorInterfaces).Select(CreateIntermediatePopulation);

        var reducedVectorGroupSpecializations = resolvedVectorGroupSpecializations.Combine(intermediateGroupPopulation).Select(VectorGroupSpecializationTypeResolution.Reduce)
            .ReportDiagnostics(context);

        var reducedVectorGroupMembers = resolvedVectorGroupMembers.Combine(intermediateGroupPopulation).Select(VectorGroupMemberResolution.Reduce)
            .ReportDiagnostics(context);

        var reducedIndividualVectorSpecializations = resolvedIndividualVectorSpecializations.Combine(intermediateIndividualPopulation)
            .Select(IndividualVectorSpecializationTypeResolution.Reduce).ReportDiagnostics(context);

        var reducedVectorGroupSpecializationInterfaces = reducedVectorGroupSpecializations.Select(ExtractInterface).Collect();
        var reducedVectorGroupMemberInterfaces = reducedVectorGroupMembers.Select(ExtractInterface).Collect();
        var reducedIndividualVectorSpecializationInterfaces = reducedIndividualVectorSpecializations.Select(ExtractInterface).Collect();

        var population = baseVectorGroupInterfaces.Combine(reducedVectorGroupSpecializationInterfaces, reducedVectorGroupMemberInterfaces,
            baseIndividualVectorInterfaces, reducedIndividualVectorSpecializationInterfaces).Select(CreatePopulation);

        return (population, new VectorGenerator(resolvedVectorGroupBases, reducedVectorGroupSpecializations, reducedVectorGroupMembers, resolvedIndividualVectorBases,
            reducedIndividualVectorSpecializations));
    }

    private static IVectorGroupType ExtractInterface(IVectorGroupType vectorGroupType, CancellationToken _) => vectorGroupType;
    private static IIntermediateVectorGroupSpecializationType ExtractInterface(IIntermediateVectorGroupSpecializationType vectorGroupType, CancellationToken _)
        => vectorGroupType;

    private static IVectorGroupMemberType ExtractInterface(IVectorGroupMemberType groupMemberType, CancellationToken _) => groupMemberType;
    private static IIntermediateVectorGroupMemberType ExtractInterface(IIntermediateVectorGroupMemberType vectorgroupMember, CancellationToken _) => vectorgroupMember;

    private static IVectorType ExtractInterface(IVectorType vectorType, CancellationToken _) => vectorType;
    private static IIntermediateIndividualVectorSpecializationType ExtractInterface(IIntermediateIndividualVectorSpecializationType vectorType, CancellationToken _)
        => vectorType;

    private static IIntermediateVectorGroupPopulation CreateIntermediatePopulation((ImmutableArray<IVectorGroupType> Bases,
        ImmutableArray<IIntermediateVectorGroupSpecializationType> Specializations, ImmutableArray<IIntermediateVectorGroupMemberType> Members) vectors, CancellationToken _)
    {
        return new IntermediateVectorGroupPopulation(vectors.Bases.ToDictionary(static (scalar) => scalar.Type.AsNamedType()),
            vectors.Specializations.ToDictionary(static (scalar) => scalar.Type.AsNamedType()), vectors.Members.ToDictionary(static (member) => member.Type.AsNamedType()));
    }

    private static IIntermediateIndividualVectorPopulation CreateIntermediatePopulation
        ((ImmutableArray<IVectorType> Bases, ImmutableArray<IIntermediateIndividualVectorSpecializationType> Specializations) vectors, CancellationToken _)
    {
        return new IntermediateIndividualVectorPopulation(vectors.Bases.ToDictionary(static (scalar) => scalar.Type.AsNamedType()),
            vectors.Specializations.ToDictionary(static (scalar) => scalar.Type.AsNamedType()));
    }

    private static IVectorPopulation CreatePopulation((ImmutableArray<IVectorGroupType> GroupBases, ImmutableArray<IVectorGroupType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorType> IndividualBases,
        ImmutableArray<IVectorType> IndividualSpecializations) vectors, CancellationToken _)
    {
        return new VectorPopulation
        (
            vectors.GroupBases.Concat(vectors.GroupSpecializations).ToDictionary(static (vector) => vector.Type.AsNamedType()),
            vectors.GroupMembers.ToDictionary(static (member) => member.Type.AsNamedType()),
            vectors.IndividualBases.Concat(vectors.IndividualSpecializations).ToDictionary(static (vector) => vector.Type.AsNamedType())
        );
    }
}
