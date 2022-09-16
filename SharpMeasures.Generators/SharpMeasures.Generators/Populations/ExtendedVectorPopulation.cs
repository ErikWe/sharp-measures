namespace SharpMeasures.Generators.Populations;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;
using System.Linq;

internal class ExtendedVectorPopulation : IVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases => vectorBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupType> Groups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers => vectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors => vectors;

    public IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup => groupMembersByGroup;

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorBaseType> vectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorType> vectors { get; }

    private ReadOnlyEquatableDictionary<NamedType, IGroupPopulation> groupMembersByGroup { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => GroupBases.Transform(static (vector) => vector as IQuantityBaseType)
        .Concat(VectorBases.Transform(static (vector) => vector as IQuantityBaseType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IQuantityType)).Concat(GroupMembers.Transform(static (vector) => vector as IQuantityType)).ToDictionary().AsEquatable();

    private ExtendedVectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroups, IReadOnlyDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers,
        IReadOnlyDictionary<NamedType, IVectorBaseType> vectorBases, IReadOnlyDictionary<NamedType, IVectorType> vectors, IReadOnlyDictionary<NamedType, IGroupPopulation> groupMembersByGroup)
    {
        this.vectorGroupBases = vectorGroupBases.AsReadOnlyEquatable();
        this.vectorBases = vectorBases.AsReadOnlyEquatable();

        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();

        this.groupMembersByGroup = groupMembersByGroup.AsReadOnlyEquatable();
    }

    public static ExtendedVectorPopulation Build(IVectorPopulation originalPopulation, IReadOnlyList<IVectorBaseType> additionalVectorBases, IReadOnlyList<IVectorSpecializationType> additionalVectorSpecializations,
        IReadOnlyList<IVectorGroupBaseType> additionalGroupBases, IReadOnlyList<IVectorGroupSpecializationType> additionalGroupSpecializations, IReadOnlyList<IVectorGroupMemberType> additionalGroupMembers)
    {
        Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation = new(originalPopulation.GroupBases.Count + additionalGroupBases.Count);
        Dictionary<NamedType, IVectorGroupSpecializationType> additionalGroupSpecializationPopulation = new(additionalGroupSpecializations.Count);
        Dictionary<NamedType, IVectorGroupMemberType> groupMemberPopulation = new(originalPopulation.GroupMembers.Count + additionalGroupMembers.Count);

        Dictionary<NamedType, Dictionary<int, IVectorGroupMemberType>> groupMembersByGroup = new(originalPopulation.GroupMembersByGroup.Count + additionalGroupBases.Count + additionalGroupSpecializations.Count);

        Dictionary<NamedType, IVectorBaseType> vectorBasePopulation = new(originalPopulation.VectorBases.Count + additionalVectorBases.Count);
        Dictionary<NamedType, IVectorSpecializationType> additionalVectorSpecializationPopulation = new(additionalVectorSpecializations.Count);

        foreach (var keyValue in originalPopulation.GroupBases)
        {
            groupBasePopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var group in originalPopulation.Groups.Keys)
        {
            groupMembersByGroup[group] = new Dictionary<int, IVectorGroupMemberType>();
        }

        foreach (var groupBase in additionalGroupBases)
        {
            if (groupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase))
            {
                groupMembersByGroup[groupBase.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();
            }
        }

        foreach (var groupSpecialization in additionalGroupSpecializations)
        {
            if (additionalGroupSpecializationPopulation.TryAdd(groupSpecialization.Type.AsNamedType(), groupSpecialization))
            {
                groupMembersByGroup[groupSpecialization.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();
            }
        }

        foreach (var keyValue in originalPopulation.GroupMembersByGroup)
        {
            var dictionary = groupMembersByGroup[keyValue.Key];

            foreach (var member in keyValue.Value.GroupMembersByDimension)
            {
                dictionary.Add(member.Key, member.Value);
            }
        }

        foreach (var keyValue in originalPopulation.GroupMembers)
        {
            groupMemberPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var groupMember in additionalGroupMembers)
        {
            if (groupMemberPopulation.TryAdd(groupMember.Type.AsNamedType(), groupMember))
            {
                if (groupMembersByGroup.TryGetValue(groupMember.Definition.VectorGroup, out var group))
                {
                    group.TryAdd(groupMember.Definition.Dimension, groupMember);
                }

                continue;
            }
        }

        foreach (var keyValue in originalPopulation.VectorBases)
        {
            vectorBasePopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var vectorBase in additionalVectorBases)
        {
            vectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase);
        }

        foreach (var vectorSpecialization in additionalVectorSpecializations)
        {
            additionalVectorSpecializationPopulation.TryAdd(vectorSpecialization.Type.AsNamedType(), vectorSpecialization);
        }

        Dictionary<NamedType, IVectorGroupType> groupPopulation = new(groupBasePopulation.Count + additionalGroupSpecializationPopulation.Count);
        Dictionary<NamedType, IVectorType> vectorPopulation = new(vectorBasePopulation.Count + additionalVectorSpecializationPopulation.Count);

        foreach (var keyValue in originalPopulation.Groups)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in groupBasePopulation)
        {
            groupPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in additionalGroupSpecializationPopulation)
        {
            groupPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in originalPopulation.Vectors)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in vectorBasePopulation)
        {
            vectorPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in additionalVectorSpecializationPopulation)
        {
            vectorPopulation.TryAdd(keyValue.Key, keyValue.Value);
        }

        var unassignedGroupSpecializations = additionalGroupSpecializationPopulation.Values.ToList();
        var unassignedVectorSpecializations = additionalVectorSpecializationPopulation.Values.ToList();

        iterativelySetBaseGroupForSpecializations();
        iterativelySetBaseVectorForSpecializations();

        return new(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, groupMembersByGroup.Transform(static (group) => new VectorGroupPopulation(group) as IGroupPopulation));

        void iterativelySetBaseGroupForSpecializations()
        {
            int startLength = unassignedGroupSpecializations.Count;

            for (int i = 0; i < unassignedGroupSpecializations.Count; i++)
            {
                if (groupBasePopulation.TryGetValue(unassignedGroupSpecializations[i].Definition.OriginalQuantity, out var groupBase))
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
                if (vectorBasePopulation.TryGetValue(unassignedVectorSpecializations[i].Definition.OriginalQuantity, out var vectorBase))
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
}
