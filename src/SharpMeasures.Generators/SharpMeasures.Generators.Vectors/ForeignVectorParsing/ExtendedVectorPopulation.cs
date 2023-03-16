namespace SharpMeasures.Generators.Vectors.ForeignVectorParsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal sealed record class ExtendedVectorPopulation : IVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases { get; }
    public IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases { get; }

    public IReadOnlyDictionary<NamedType, IVectorGroupType> Groups { get; }
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers { get; }
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors { get; }

    public IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases
    {
        get
        {
            Dictionary<NamedType, IQuantityBaseType> quantityBases = new(GroupBases.Count + VectorBases.Count);

            foreach (var group in GroupBases)
            {
                quantityBases.Add(group.Key, group.Value);
            }

            foreach (var vector in VectorBases)
            {
                quantityBases.TryAdd(vector.Key, vector.Value);
            }

            return quantityBases;
        }
    }

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities
    {
        get
        {
            Dictionary<NamedType, IQuantityType> quantities = new(Groups.Count + Vectors.Count + GroupMembers.Count);

            foreach (var group in Groups)
            {
                quantities.Add(group.Key, group.Value);
            }

            foreach (var vector in Vectors)
            {
                quantities.TryAdd(vector.Key, vector.Value);
            }

            foreach (var member in GroupMembers)
            {
                quantities.TryAdd(member.Key, member.Value);
            }

            return quantities;
        }
    }

    private ExtendedVectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupBaseType> groupBases, IReadOnlyDictionary<NamedType, IVectorGroupType> groups, IReadOnlyDictionary<NamedType, IVectorGroupMemberType> groupMembers,
        IReadOnlyDictionary<NamedType, IVectorBaseType> vectorBases, IReadOnlyDictionary<NamedType, IVectorType> vectors, IReadOnlyDictionary<NamedType, IGroupPopulation> groupMembersByGroup)
    {
        GroupBases = groupBases.AsReadOnlyEquatable();
        VectorBases = vectorBases.AsReadOnlyEquatable();

        Groups = groups.AsReadOnlyEquatable();
        GroupMembers = groupMembers.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();

        GroupMembersByGroup = groupMembersByGroup.AsReadOnlyEquatable();
    }

    public static ExtendedVectorPopulation Build(IVectorPopulation originalPopulation, ForeignVectorProcessingResult foreignPopulation)
    {
        Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation = new(originalPopulation.GroupBases.Count + foreignPopulation.GroupBases.Count);
        Dictionary<NamedType, IVectorGroupSpecializationType> additionalGroupSpecializationPopulation = new(foreignPopulation.GroupSpecializations.Count);
        Dictionary<NamedType, IVectorGroupMemberType> groupMemberPopulation = new(originalPopulation.GroupMembers.Count + foreignPopulation.GroupMembers.Count);

        Dictionary<NamedType, Dictionary<int, IVectorGroupMemberType>> groupMembersByGroup = new(originalPopulation.GroupMembersByGroup.Count + foreignPopulation.GroupBases.Count + foreignPopulation.GroupSpecializations.Count);

        Dictionary<NamedType, IVectorBaseType> vectorBasePopulation = new(originalPopulation.VectorBases.Count + foreignPopulation.VectorBases.Count);
        Dictionary<NamedType, IVectorSpecializationType> additionalVectorSpecializationPopulation = new(foreignPopulation.VectorSpecializations.Count);

        foreach (var keyValue in originalPopulation.GroupBases)
        {
            groupBasePopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var group in originalPopulation.Groups.Keys)
        {
            groupMembersByGroup[group] = new Dictionary<int, IVectorGroupMemberType>();
        }

        foreach (var groupBase in foreignPopulation.GroupBases)
        {
            if (groupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase))
            {
                groupMembersByGroup[groupBase.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();
            }
        }

        foreach (var groupSpecialization in foreignPopulation.GroupSpecializations)
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

        foreach (var groupMember in foreignPopulation.GroupMembers)
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

        foreach (var vectorBase in foreignPopulation.VectorBases)
        {
            vectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase);
        }

        foreach (var vectorSpecialization in foreignPopulation.VectorSpecializations)
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

        List<IVectorGroupSpecializationType> unassignedGroupSpecializations = new(additionalGroupSpecializationPopulation.Count + (originalPopulation.Groups.Count - originalPopulation.GroupBases.Count));
        List<IVectorSpecializationType> unassignedVectorSpecializations = new(additionalVectorSpecializationPopulation.Count + (originalPopulation.Vectors.Count - originalPopulation.VectorBases.Count));

        foreach (var additionalGroupSpecialization in additionalGroupSpecializationPopulation)
        {
            unassignedGroupSpecializations.Add(additionalGroupSpecialization.Value);
        }

        if (originalPopulation.GroupBases.Count != originalPopulation.Groups.Count)
        {
            foreach (var group in originalPopulation.Groups)
            {
                if (originalPopulation.GroupBases.ContainsKey(group.Key) is false)
                {
                    unassignedGroupSpecializations.Add((IVectorGroupSpecializationType)group.Value);
                }
            }
        }

        foreach (var additionalVectorSpecialization in additionalVectorSpecializationPopulation)
        {
            unassignedVectorSpecializations.Add(additionalVectorSpecialization.Value);
        }

        if (originalPopulation.VectorBases.Count != originalPopulation.Vectors.Count)
        {
            foreach (var vector in originalPopulation.Vectors)
            {
                if (originalPopulation.GroupBases.ContainsKey(vector.Key) is false)
                {
                    unassignedVectorSpecializations.Add((IVectorSpecializationType)vector.Value);
                }
            }
        }

        iterativelySetBaseGroupForSpecializations();
        iterativelySetBaseVectorForSpecializations();

        return new(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, (IReadOnlyDictionary<NamedType, IGroupPopulation>)groupMembersByGroup.Transform(static (group) => new GroupPopulation(group) as IGroupPopulation));

        void iterativelySetBaseGroupForSpecializations()
        {
            var startLength = unassignedGroupSpecializations.Count;

            for (var i = 0; i < unassignedGroupSpecializations.Count; i++)
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
            var startLength = unassignedVectorSpecializations.Count;

            for (var i = 0; i < unassignedVectorSpecializations.Count; i++)
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
