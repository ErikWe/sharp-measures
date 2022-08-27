namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;
using System.Linq;

internal class VectorPopulation : IVectorPopulationWithData
{
    public static VectorPopulation Build(IReadOnlyList<IVectorBaseType> vectorBases, IReadOnlyList<IVectorSpecializationType> vectorSpecializations, IReadOnlyList<IVectorGroupBaseType> groupBases,
        IReadOnlyList<IVectorGroupSpecializationType> groupSpecializations, IReadOnlyList<IVectorGroupMemberType> groupMembers)
    {
        Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation = new(groupBases.Count);
        Dictionary<NamedType, IVectorGroupType> groupPopulation = new(groupBases.Count + groupSpecializations.Count);
        Dictionary<NamedType, IVectorGroupMemberType> groupMemberPopulation = new(groupMembers.Count);

        Dictionary<NamedType, Dictionary<int, IVectorGroupMemberType>> groupMembersByGroup = new(groupBases.Count + groupSpecializations.Count);

        Dictionary<NamedType, IVectorBaseType> vectorBasePopulation = new(vectorBases.Count);
        Dictionary<NamedType, IVectorType> vectorPopulation = new(vectorBases.Count + vectorSpecializations.Count);

        Dictionary<NamedType, IVectorGroupType> duplicateGroupPopulation = new();
        Dictionary<NamedType, IVectorGroupMemberType> duplicateGroupMemberPopulation = new();
        Dictionary<NamedType, IVectorType> duplicateVectorPopulation = new();

        foreach (var group in (groupBases as IEnumerable<IVectorGroupType>).Concat(groupSpecializations))
        {
            if (groupPopulation.TryAdd(group.Type.AsNamedType(), group))
            {
                groupMembersByGroup[group.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();

                continue;
            }

            duplicateGroupPopulation.TryAdd(group.Type.AsNamedType(), group);
        }

        foreach (var groupBase in groupBases)
        {
            groupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase);
        }

        foreach (var groupMember in groupMembers)
        {
            if (groupMemberPopulation.TryAdd(groupMember.Type.AsNamedType(), groupMember))
            {
                if (groupMembersByGroup.TryGetValue(groupMember.Definition.VectorGroup, out var group))
                {
                    group.TryAdd(groupMember.Definition.Dimension, groupMember);
                }

                continue;
            }

            duplicateGroupMemberPopulation.TryAdd(groupMember.Type.AsNamedType(), groupMember);
        }

        foreach (var vector in (vectorBases as IEnumerable<IVectorType>).Concat(vectorSpecializations))
        {
            if (vectorPopulation.TryAdd(vector.Type.AsNamedType(), vector))
            {
                continue;
            }

            duplicateVectorPopulation.TryAdd(vector.Type.AsNamedType(), vector);
        }

        foreach (var vectorBase in vectorBases)
        {
            vectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase);
        }

        var unassignedGroupSpecializations = groupSpecializations.ToList();
        var unassignedVectorSpecializations = vectorSpecializations.ToList();

        iterativelySetBaseGroupForSpecializations();
        iterativelySetBaseVectorForSpecializations();

        return new(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, groupMembersByGroup.Transform(static (group) => new GroupPopulation(group) as IGroupPopulation), duplicateGroupPopulation, duplicateGroupMemberPopulation, duplicateVectorPopulation);

        void iterativelySetBaseGroupForSpecializations()
        {
            int startLength = unassignedGroupSpecializations.Count;

            for (int i = 0; i < unassignedGroupSpecializations.Count; i++)
            {
                if (groupBasePopulation.TryGetValue(unassignedGroupSpecializations[i].Definition.OriginalVectorGroup, out var groupBase))
                {
                    groupBasePopulation.TryAdd(unassignedGroupSpecializations[i].Type.AsNamedType(), groupBase);

                    unassignedGroupSpecializations.RemoveAt(i);
                    i -= 1;
                }
            }

            if (startLength != unassignedGroupSpecializations.Count)
            {
                iterativelySetBaseVectorForSpecializations();
            }
        }

        void iterativelySetBaseVectorForSpecializations()
        {
            int startLength = unassignedVectorSpecializations.Count;

            for (int i = 0; i < unassignedVectorSpecializations.Count; i++)
            {
                if (vectorBasePopulation.TryGetValue(unassignedVectorSpecializations[i].Definition.OriginalVector, out var vectorBase))
                {
                    vectorBasePopulation.TryAdd(unassignedVectorSpecializations[i].Type.AsNamedType(), vectorBase);

                    unassignedVectorSpecializations.RemoveAt(i);
                    i -= 1;
                }
            }

            if (startLength != unassignedVectorSpecializations.Count)
            {
                iterativelySetBaseVectorForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases => vectorBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupType> Groups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers => vectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors => vectors;

    public IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup => groupMembersByGroup;

    public IReadOnlyDictionary<NamedType, IVectorGroupType> DuplicatelyDefinedGroups => duplicatelyDefinedGroups;
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers => duplicatelyDefinedGroupMembers;
    public IReadOnlyDictionary<NamedType, IVectorType> DuplicatelyDefinedVectors => duplicatelyDefinedVectors;

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorBaseType> vectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorType> vectors { get; }

    private ReadOnlyEquatableDictionary<NamedType, IGroupPopulation> groupMembersByGroup { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> duplicatelyDefinedGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorType> duplicatelyDefinedVectors { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => GroupBases.Transform(static (vector) => vector as IQuantityBaseType)
        .Concat(VectorBases.Transform(static (vector) => vector as IQuantityBaseType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IQuantityType)).Concat(GroupMembers.Transform(static (vector) => vector as IQuantityType)).ToDictionary().AsEquatable();

    private VectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroups,
        IReadOnlyDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers, IReadOnlyDictionary<NamedType, IVectorBaseType> vectorBases,
        IReadOnlyDictionary<NamedType, IVectorType> vectors, IReadOnlyDictionary<NamedType, IGroupPopulation> groupMembersByGroup,
        IReadOnlyDictionary<NamedType, IVectorGroupType> duplicatelyDefinedGroups, IReadOnlyDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers,
        IReadOnlyDictionary<NamedType, IVectorType> duplicatelyDefinedVectors)
    {
        this.vectorGroupBases = vectorGroupBases.AsReadOnlyEquatable();
        this.vectorBases = vectorBases.AsReadOnlyEquatable();

        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();

        this.groupMembersByGroup = groupMembersByGroup.AsReadOnlyEquatable();

        this.duplicatelyDefinedGroups = duplicatelyDefinedGroups.AsReadOnlyEquatable();
        this.duplicatelyDefinedGroupMembers = duplicatelyDefinedGroupMembers.AsReadOnlyEquatable();
        this.duplicatelyDefinedVectors = duplicatelyDefinedVectors.AsReadOnlyEquatable();
    }
}
