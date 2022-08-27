namespace SharpMeasures.Generators.Vectors.Parsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Immutable;
using System.Linq;
using System.Threading;

public interface IVectorValidator
{
    public abstract (IncrementalValueProvider<IVectorPopulation>, IVectorResolver) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider);
}

internal class VectorValidator : IVectorValidator
{
    private IncrementalValueProvider<IVectorPopulationWithData> VectorPopulationProvider { get; }

    private IncrementalValuesProvider<GroupBaseType> GroupBaseProvider { get; }
    private IncrementalValuesProvider<GroupSpecializationType> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<GroupMemberType> GroupMemberProvider { get; }

    private IncrementalValuesProvider<VectorBaseType> VectorBaseProvider { get; }
    private IncrementalValuesProvider<VectorSpecializationType> VectorSpecializationProvider { get; }

    internal VectorValidator(IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider, IncrementalValuesProvider<GroupBaseType> groupBaseProvider,
        IncrementalValuesProvider<GroupSpecializationType> groupSpecializationProvider, IncrementalValuesProvider<GroupMemberType> groupMemberProvider,
        IncrementalValuesProvider<VectorBaseType> vectorBaseProvider, IncrementalValuesProvider<VectorSpecializationType> vectorSpecializationProvider)
    {
        VectorPopulationProvider = vectorPopulationProvider;

        GroupBaseProvider = groupBaseProvider;
        GroupSpecializationProvider = groupSpecializationProvider;
        GroupMemberProvider = groupMemberProvider;

        VectorBaseProvider = vectorBaseProvider;
        VectorSpecializationProvider = vectorSpecializationProvider;
    }

    public (IncrementalValueProvider<IVectorPopulation>, IVectorResolver) Validate(IncrementalGeneratorInitializationContext context, IncrementalValueProvider<IUnitPopulation> unitPopulationProvider,
        IncrementalValueProvider<IScalarPopulation> scalarPopulationProvider)
    {
        var validatedGroupBases = GroupBaseValidator.Validate(context, GroupBaseProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider);
        var validatedGroupSpecializations = GroupSpecializationValidator.Validate(context, GroupSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider);
        var validatedGroupMembers = GroupMemberValidator.Validate(context, GroupMemberProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider);

        var validatedVectorBases = VectorBaseValidator.Validate(context, VectorBaseProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider);
        var validatedVectorSpecializations = VectorSpecializationValidator.Validate(context, VectorSpecializationProvider, unitPopulationProvider, scalarPopulationProvider, VectorPopulationProvider);

        var groupBaseInterfaces = validatedGroupBases.Select(ExtractInterface).Collect();
        var groupSpecializationInterfaces = validatedGroupSpecializations.Select(ExtractInterface).Collect();
        var groupMemberInterfaces = validatedGroupMembers.Select(ExtractInterface).Collect();

        var vectorBaseInterfaces = validatedVectorBases.Select(ExtractInterface).Collect();
        var vectorSpecializationInterfaces = validatedVectorSpecializations.Select(ExtractInterface).Collect();

        var populationWithData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new VectorResolver(populationWithData, validatedGroupBases, validatedGroupSpecializations, validatedGroupMembers,
            validatedVectorBases, validatedVectorSpecializations));
    }

    private static IVectorGroupBaseType ExtractInterface(IVectorGroupBaseType groupType, CancellationToken _) => groupType;
    private static IVectorGroupSpecializationType ExtractInterface(IVectorGroupSpecializationType groupType, CancellationToken _) => groupType;
    private static IVectorGroupMemberType ExtractInterface(IVectorGroupMemberType groupMemberType, CancellationToken _) => groupMemberType;

    private static IVectorBaseType ExtractInterface(IVectorBaseType vectorType, CancellationToken _) => vectorType;
    private static IVectorSpecializationType ExtractInterface(IVectorSpecializationType vectorType, CancellationToken _) => vectorType;

    private static IVectorPopulation ReducePopulation(IVectorPopulationWithData vectorPopulation, CancellationToken _) => vectorPopulation;

    private static IVectorPopulationWithData CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
