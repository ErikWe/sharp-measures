namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedVectorPopulation : IUnresolvedVectorPopulationWithData
{
    public static UnresolvedVectorPopulation Build(ImmutableArray<IUnresolvedVectorGroupBaseType> vectorGroupBases,
        ImmutableArray<IUnresolvedVectorGroupSpecializationType> vectorGroupSpecializations, ImmutableArray<IUnresolvedVectorGroupMemberType> vectorGroupMembers,
        ImmutableArray<IUnresolvedIndividualVectorBaseType> individualVectorBases, ImmutableArray<IUnresolvedIndividualVectorSpecializationType> individualVectorSpecializations)
    {
        Dictionary<NamedType, IUnresolvedVectorGroupType> vectorGroupPopulation = new(vectorGroupBases.Length + vectorGroupSpecializations.Length);
        Dictionary<NamedType, IUnresolvedVectorGroupBaseType> vectorGroupBasePopulation = new(vectorGroupBases.Length + individualVectorBases.Length);
        Dictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMemberPopulation = new(vectorGroupMembers.Length);

        Dictionary<NamedType, Dictionary<int, IUnresolvedVectorGroupMemberType>> vectorGroupMembersByGroup = new(vectorGroupBases.Length + vectorGroupSpecializations.Length);

        Dictionary<NamedType, IUnresolvedIndividualVectorType> individualVectorPopulation = new(individualVectorBases.Length);
        Dictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBasePopulation = new(individualVectorBases.Length);

        Dictionary<NamedType, IUnresolvedVectorGroupType> duplicateVectorGroupPopulation = new();
        Dictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicateVectorGroupMemberPopulation = new();
        Dictionary<NamedType, IUnresolvedIndividualVectorType> duplicateIndividualVectorPopulation = new();

        foreach (var vectorGroup in (vectorGroupBases as IEnumerable<IUnresolvedVectorGroupType>).Concat(vectorGroupSpecializations))
        {
            if (vectorGroupPopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup) is false)
            {
                duplicateVectorGroupPopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup);
            }

            vectorGroupMembersByGroup.TryAdd(vectorGroup.Type.AsNamedType(), new Dictionary<int, IUnresolvedVectorGroupMemberType>());
        }

        foreach (var vectorGroupBase in vectorGroupBases)
        {
            vectorGroupBasePopulation.TryAdd(vectorGroupBase.Type.AsNamedType(), vectorGroupBase);
        }

        foreach (var individualVector in (individualVectorBases as IEnumerable<IUnresolvedIndividualVectorType>).Concat(individualVectorSpecializations))
        {
            if (individualVectorPopulation.TryAdd(individualVector.Type.AsNamedType(), individualVector) is false)
            {
                duplicateIndividualVectorPopulation.TryAdd(individualVector.Type.AsNamedType(), individualVector);
            }
        }

        foreach (var individualVectorBase in individualVectorBases)
        {
            individualVectorBasePopulation.TryAdd(individualVectorBase.Type.AsNamedType(), individualVectorBase);
        }

        foreach (var vectorGroupMember in vectorGroupMembers)
        {
            if (vectorGroupMemberPopulation.TryAdd(vectorGroupMember.Type.AsNamedType(), vectorGroupMember) is false)
            {
                duplicateVectorGroupMemberPopulation.TryAdd(vectorGroupMember.Type.AsNamedType(), vectorGroupMember);
            }

            if (vectorGroupMembersByGroup.TryGetValue(vectorGroupMember.Definition.VectorGroup, out var vectorGroupMembersInGroup))
            {
                vectorGroupMembersInGroup.TryAdd(vectorGroupMember.Definition.Dimension, vectorGroupMember);
            }
        }

        var unassignedVectorGroupSpecializations = vectorGroupSpecializations.ToList();
        var unassignedIndividualVectorSpecializations = individualVectorSpecializations.ToList();

        iterativelySetBaseVectorGroupForSpecializations();
        iterativelySetBaseIndividualVectorForSpecializations();

        return new(vectorGroupPopulation, vectorGroupBasePopulation, vectorGroupMemberPopulation,
            vectorGroupMembersByGroup.Transform(static (members) => new UnresolvedVectorGroupPopulation(members) as IUnresolvedVectorGroupPopulation),
            individualVectorPopulation, individualVectorBasePopulation, duplicateVectorGroupPopulation, duplicateVectorGroupMemberPopulation, duplicateIndividualVectorPopulation);

        void iterativelySetBaseVectorGroupForSpecializations()
        {
            int startLength = unassignedVectorGroupSpecializations.Count;

            for (int i = 0; i < unassignedVectorGroupSpecializations.Count; i++)
            {
                if (vectorGroupBasePopulation.TryGetValue(unassignedVectorGroupSpecializations[i].Definition.OriginalVectorGroup, out var vectorGroupBase))
                {
                    vectorGroupBasePopulation.TryAdd(unassignedVectorGroupSpecializations[i].Type.AsNamedType(), vectorGroupBase);

                    unassignedVectorGroupSpecializations.RemoveAt(i);
                    i -= 1;
                }
            }

            if (startLength != unassignedVectorGroupSpecializations.Count)
            {
                iterativelySetBaseIndividualVectorForSpecializations();
            }
        }

        void iterativelySetBaseIndividualVectorForSpecializations()
        {
            int startLength = unassignedIndividualVectorSpecializations.Count;

            for (int i = 0; i < unassignedIndividualVectorSpecializations.Count; i++)
            {
                if (individualVectorBasePopulation.TryGetValue(unassignedIndividualVectorSpecializations[i].Definition.OriginalVector, out var individualVectorBase))
                {
                    individualVectorBasePopulation.TryAdd(unassignedIndividualVectorSpecializations[i].Type.AsNamedType(), individualVectorBase);

                    unassignedIndividualVectorSpecializations.RemoveAt(i);
                    i -= 1;
                }
            }

            if (startLength != unassignedIndividualVectorSpecializations.Count)
            {
                iterativelySetBaseIndividualVectorForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> VectorGroups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> VectorGroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupPopulation> VectorGroupMembersByGroup => vectorGroupMembersByGroup;

    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> IndividualVectors => individualVectors;
    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> IndividualVectorBases => individualVectorBases;

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> DuplicatelyDefinedVectorGroups => duplicatelyDefinedVectorGroups;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers => duplicatelyDefinedVectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> DuplicatelyDefinedIndividualVectors => duplicatelyDefinedIndividualVectors;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities => VectorGroups.Transform(static (vector) => vector as IUnresolvedQuantityType)
        .Concat(IndividualVectors.Transform(static (vector) => vector as IUnresolvedQuantityType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityBaseType> IUnresolvedQuantityPopulation.QuantityBases => VectorGroupBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)
        .Concat(IndividualVectorBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)).ToDictionary().AsEquatable();

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupBaseType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupPopulation> vectorGroupMembersByGroup { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorType> individualVectors { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupType> duplicatelyDefinedVectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorType> duplicatelyDefinedIndividualVectors { get; }

    public UnresolvedVectorPopulation(IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> vectorGroupBases,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupPopulation> vectorGroupMembersByGroup,
        IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> individualVectors, IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBases,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> duplicatelyDefinedVectorGroups, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers,
        IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> duplicatelyDefinedIndividualVectors)
    {
        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.vectorGroupBases = vectorGroupBases.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();

        this.vectorGroupMembersByGroup = vectorGroupMembersByGroup.AsReadOnlyEquatable();

        this.individualVectors = individualVectors.AsReadOnlyEquatable();
        this.individualVectorBases = individualVectorBases.AsReadOnlyEquatable();

        this.duplicatelyDefinedVectorGroups = duplicatelyDefinedVectorGroups.AsReadOnlyEquatable();
        this.duplicatelyDefinedVectorGroupMembers = duplicatelyDefinedVectorGroupMembers.AsReadOnlyEquatable();
        this.duplicatelyDefinedIndividualVectors = duplicatelyDefinedIndividualVectors.AsReadOnlyEquatable();
    }
}
