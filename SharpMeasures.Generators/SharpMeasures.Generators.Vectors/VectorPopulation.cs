namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing;

using System.Collections.Generic;
using System.Linq;

internal sealed record class VectorPopulation : IVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases { get; }
    public IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases { get; }

    public IReadOnlyDictionary<NamedType, IVectorGroupType> Groups { get; }
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers { get; }
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors { get; }

    public IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => GroupBases.Transform(static (vector) => vector as IQuantityBaseType) .Concat(VectorBases.Transform(static (vector) => vector as IQuantityBaseType)).ToDictionary().AsEquatable();
    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IQuantityType) .Concat(Vectors.Transform(static (vector) => vector as IQuantityType)).Concat(GroupMembers.Transform(static (vector) => vector as IQuantityType)).ToDictionary().AsEquatable();

    private VectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupType> groups, IReadOnlyDictionary<NamedType, IVectorGroupMemberType> groupMembers,
        IReadOnlyDictionary<NamedType, IVectorBaseType> vectorBases, IReadOnlyDictionary<NamedType, IVectorType> vectors, IReadOnlyDictionary<NamedType, IGroupPopulation> groupMembersByGroup)
    {
        GroupBases = vectorGroupBases.AsReadOnlyEquatable();
        VectorBases = vectorBases.AsReadOnlyEquatable();

        Groups = groups.AsReadOnlyEquatable();
        GroupMembers = groupMembers.AsReadOnlyEquatable();
        Vectors = vectors.AsReadOnlyEquatable();

        GroupMembersByGroup = groupMembersByGroup.AsReadOnlyEquatable();
    }

    public static (VectorPopulation Population, VectorProcessingData ProcessingData) Build(IReadOnlyList<IVectorBaseType> vectorBases, IReadOnlyList<IVectorSpecializationType> vectorSpecializations, IReadOnlyList<IVectorGroupBaseType> groupBases, IReadOnlyList<IVectorGroupSpecializationType> groupSpecializations, IReadOnlyList<IVectorGroupMemberType> groupMembers)
    {
        Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation = new(groupBases.Count);
        Dictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationPopulation = new(groupSpecializations.Count);
        Dictionary<NamedType, IVectorGroupMemberType> groupMemberPopulation = new(groupMembers.Count);

        Dictionary<NamedType, Dictionary<int, IVectorGroupMemberType>> groupMembersByGroup = new(groupBases.Count + groupSpecializations.Count);

        Dictionary<NamedType, IVectorBaseType> vectorBasePopulation = new(vectorBases.Count);
        Dictionary<NamedType, IVectorSpecializationType> vectorSpecializationPopulation = new(vectorSpecializations.Count);

        Dictionary<NamedType, IVectorGroupBaseType> duplicateGroupBasePopulation = new();
        Dictionary<NamedType, IVectorGroupSpecializationType> duplicateGroupSpecializationPopulation = new();
        Dictionary<NamedType, IVectorGroupMemberType> duplicateGroupMemberPopulation = new();

        Dictionary<NamedType, IVectorBaseType> duplicateVectorBasePopulation = new();
        Dictionary<NamedType, IVectorSpecializationType> duplicateVectorSpecializationPopulation = new();

        Dictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases = new();
        Dictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases = new();

        foreach (var groupBase in groupBases)
        {
            if (groupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase))
            {
                groupMembersByGroup[groupBase.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();

                continue;
            }

            duplicateGroupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase);
        }

        foreach (var groupSpecialization in groupSpecializations)
        {
            if (groupBasePopulation.ContainsKey(groupSpecialization.Type.AsNamedType()))
            {
                groupSpecializationsAlreadyDefinedAsGroupBases.TryAdd(groupSpecialization.Type.AsNamedType(), groupSpecialization);

                continue;
            }

            if (groupSpecializationPopulation.TryAdd(groupSpecialization.Type.AsNamedType(), groupSpecialization))
            {
                groupMembersByGroup[groupSpecialization.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();

                continue;
            }

            duplicateGroupSpecializationPopulation.TryAdd(groupSpecialization.Type.AsNamedType(), groupSpecialization);
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

        foreach (var vectorBase in vectorBases)
        {
            if (vectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase))
            {
                continue;
            }

            duplicateVectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase);
        }

        foreach (var vectorSpecialization in vectorSpecializations)
        {
            if (vectorBasePopulation.ContainsKey(vectorSpecialization.Type.AsNamedType()))
            {
                vectorSpecializationsAlreadyDefinedAsVectorBases.TryAdd(vectorSpecialization.Type.AsNamedType(), vectorSpecialization);

                continue;
            }

            if (vectorSpecializationPopulation.TryAdd(vectorSpecialization.Type.AsNamedType(), vectorSpecialization))
            {
                continue;
            }

            duplicateVectorSpecializationPopulation.TryAdd(vectorSpecialization.Type.AsNamedType(), vectorSpecialization);
        }

        Dictionary<NamedType, IVectorGroupType> groupPopulation = new(groupBasePopulation.Count + groupSpecializationPopulation.Count);
        Dictionary<NamedType, IVectorType> vectorPopulation = new(vectorBasePopulation.Count + vectorSpecializationPopulation.Count);

        foreach (var keyValue in groupBasePopulation)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in groupSpecializationPopulation)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in vectorBasePopulation)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in vectorSpecializationPopulation)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        IterativelySetGroupBaseForSpecializations(groupBasePopulation, groupSpecializationPopulation);
        IterativelySetVectorBaseForSpecializations(vectorBasePopulation, vectorSpecializationPopulation);

        var population = new VectorPopulation(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, groupMembersByGroup.Transform(static (group) => new GroupPopulation(group) as IGroupPopulation));
        var processingData = new VectorProcessingData(duplicateVectorBasePopulation, duplicateVectorSpecializationPopulation, vectorSpecializationsAlreadyDefinedAsVectorBases, duplicateGroupBasePopulation, duplicateGroupSpecializationPopulation, groupSpecializationsAlreadyDefinedAsGroupBases, duplicateGroupMemberPopulation);

        return (population, processingData);
    }

    public static VectorPopulation BuildWithoutProcessingData(IReadOnlyList<IVectorBaseType> vectorBases, IReadOnlyList<IVectorSpecializationType> vectorSpecializations, IReadOnlyList<IVectorGroupBaseType> groupBases, IReadOnlyList<IVectorGroupSpecializationType> groupSpecializations, IReadOnlyList<IVectorGroupMemberType> groupMembers)
    {
        Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation = new(groupBases.Count);
        Dictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationPopulation = new(groupSpecializations.Count);
        Dictionary<NamedType, IVectorGroupMemberType> groupMemberPopulation = new(groupMembers.Count);

        Dictionary<NamedType, Dictionary<int, IVectorGroupMemberType>> groupMembersByGroup = new(groupBases.Count + groupSpecializations.Count);

        Dictionary<NamedType, IVectorBaseType> vectorBasePopulation = new(vectorBases.Count);
        Dictionary<NamedType, IVectorSpecializationType> vectorSpecializationPopulation = new(vectorSpecializations.Count);

        foreach (var groupBase in groupBases)
        {
            if (groupBasePopulation.TryAdd(groupBase.Type.AsNamedType(), groupBase))
            {
                groupMembersByGroup[groupBase.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();
            }
        }

        foreach (var groupSpecialization in groupSpecializations)
        {
            if (groupSpecializationPopulation.TryAdd(groupSpecialization.Type.AsNamedType(), groupSpecialization))
            {
                groupMembersByGroup[groupSpecialization.Type.AsNamedType()] = new Dictionary<int, IVectorGroupMemberType>();
            }
        }

        foreach (var groupMember in groupMembers)
        {
            if (groupMemberPopulation.TryAdd(groupMember.Type.AsNamedType(), groupMember))
            {
                if (groupMembersByGroup.TryGetValue(groupMember.Definition.VectorGroup, out var group))
                {
                    group.TryAdd(groupMember.Definition.Dimension, groupMember);
                }
            }
        }

        foreach (var vectorBase in vectorBases)
        {
            vectorBasePopulation.TryAdd(vectorBase.Type.AsNamedType(), vectorBase);
        }

        foreach (var vectorSpecialization in vectorSpecializations)
        {
            vectorSpecializationPopulation.TryAdd(vectorSpecialization.Type.AsNamedType(), vectorSpecialization);
        }

        Dictionary<NamedType, IVectorGroupType> groupPopulation = new(groupBasePopulation.Count + groupSpecializationPopulation.Count);
        Dictionary<NamedType, IVectorType> vectorPopulation = new(vectorBasePopulation.Count + vectorSpecializationPopulation.Count);

        foreach (var keyValue in groupBasePopulation)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in groupSpecializationPopulation)
        {
            groupPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in vectorBasePopulation)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        foreach (var keyValue in vectorSpecializationPopulation)
        {
            vectorPopulation.Add(keyValue.Key, keyValue.Value);
        }

        IterativelySetGroupBaseForSpecializations(groupBasePopulation, groupSpecializationPopulation);
        IterativelySetVectorBaseForSpecializations(vectorBasePopulation, vectorSpecializationPopulation);

        return new(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, groupMembersByGroup.Transform(static (group) => new GroupPopulation(group) as IGroupPopulation));
    }

    private static void IterativelySetGroupBaseForSpecializations(Dictionary<NamedType, IVectorGroupBaseType> groupBasePopulation, IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationPopulation)
    {
        var unassignedGroupSpecializations = groupSpecializationPopulation.Values.ToList();

        iterativelySetGroupBaseForSpecializations();

        void iterativelySetGroupBaseForSpecializations()
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
                iterativelySetGroupBaseForSpecializations();
            }
        }
    }

    private static void IterativelySetVectorBaseForSpecializations(Dictionary<NamedType, IVectorBaseType> vectorBasePopulation, IReadOnlyDictionary<NamedType, IVectorSpecializationType> vectorSpecializationPopulation)
    {
        var unassignedVectorSpecializations = vectorSpecializationPopulation.Values.ToList();

        iterativelySetVectorBaseForSpecializations();

        void iterativelySetVectorBaseForSpecializations()
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
                iterativelySetVectorBaseForSpecializations();
            }
        }
    }
}
