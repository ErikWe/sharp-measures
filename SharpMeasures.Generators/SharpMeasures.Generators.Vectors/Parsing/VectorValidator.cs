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

    private IncrementalValuesProvider<Optional<GroupBaseType>> GroupBaseProvider { get; }
    private IncrementalValuesProvider<Optional<GroupSpecializationType>> GroupSpecializationProvider { get; }
    private IncrementalValuesProvider<Optional<GroupMemberType>> GroupMemberProvider { get; }

    private IncrementalValuesProvider<Optional<VectorBaseType>> VectorBaseProvider { get; }
    private IncrementalValuesProvider<Optional<VectorSpecializationType>> VectorSpecializationProvider { get; }

    internal VectorValidator(IncrementalValueProvider<IVectorPopulationWithData> vectorPopulationProvider, IncrementalValuesProvider<Optional<GroupBaseType>> groupBaseProvider,
        IncrementalValuesProvider<Optional<GroupSpecializationType>> groupSpecializationProvider, IncrementalValuesProvider<Optional<GroupMemberType>> groupMemberProvider,
        IncrementalValuesProvider<Optional<VectorBaseType>> vectorBaseProvider, IncrementalValuesProvider<Optional<VectorSpecializationType>> vectorSpecializationProvider)
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

        var groupBaseInterfaces = validatedGroupBases.Select(ExtractInterface).CollectResults();
        var groupSpecializationInterfaces = validatedGroupSpecializations.Select(ExtractInterface).CollectResults();
        var groupMemberInterfaces = validatedGroupMembers.Select(ExtractInterface).CollectResults();

        var vectorBaseInterfaces = validatedVectorBases.Select(ExtractInterface).CollectResults();
        var vectorSpecializationInterfaces = validatedVectorSpecializations.Select(ExtractInterface).CollectResults();

        var populationWithData = groupBaseInterfaces.Combine(groupSpecializationInterfaces, groupMemberInterfaces, vectorBaseInterfaces, vectorSpecializationInterfaces).Select(CreatePopulation);

        return (populationWithData.Select(ReducePopulation), new VectorResolver(populationWithData, validatedGroupBases, validatedGroupSpecializations, validatedGroupMembers,
            validatedVectorBases, validatedVectorSpecializations));
    }

    private static Optional<IVectorGroupBaseType> ExtractInterface(Optional<GroupBaseType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupBaseType>();
    private static Optional<IVectorGroupSpecializationType> ExtractInterface(Optional<GroupSpecializationType> groupType, CancellationToken _) => groupType.HasValue ? groupType.Value : new Optional<IVectorGroupSpecializationType>();
    private static Optional<IVectorGroupMemberType> ExtractInterface(Optional<GroupMemberType> groupMemberType, CancellationToken _) => groupMemberType.HasValue ? groupMemberType.Value : new Optional<IVectorGroupMemberType>();

    private static Optional<IVectorBaseType> ExtractInterface(Optional<VectorBaseType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorBaseType>();
    private static Optional<IVectorSpecializationType> ExtractInterface(Optional<VectorSpecializationType> vectorType, CancellationToken _) => vectorType.HasValue ? vectorType.Value : new Optional<IVectorSpecializationType>();

    private static IVectorPopulation ReducePopulation(IVectorPopulationWithData vectorPopulation, CancellationToken _) => vectorPopulation;

    private static IVectorPopulationWithData CreatePopulation((ImmutableArray<IVectorGroupBaseType> GroupBases, ImmutableArray<IVectorGroupSpecializationType> GroupSpecializations,
        ImmutableArray<IVectorGroupMemberType> GroupMembers, ImmutableArray<IVectorBaseType> VectorBases, ImmutableArray<IVectorSpecializationType> VectorSpecializations) vectors, CancellationToken _)
    {
        return VectorPopulation.Build(vectors.VectorBases, vectors.VectorSpecializations, vectors.GroupBases, vectors.GroupSpecializations, vectors.GroupMembers);
    }
}
