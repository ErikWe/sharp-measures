namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;
using System.Linq;

internal class VectorPopulation : IVectorPopulationWithData
{
    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases => vectorBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupType> Groups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers => vectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IVectorType> Vectors => vectors;

    public IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup => groupMembersByGroup;

    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> DuplicatelyDefinedGroupBases => duplicatelyDefinedGroupBases;
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> DuplicatelyDefinedGroupSpecializations => duplicatelyDefinedGroupSpecializations;
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> GroupSpecializationsAlreadyDefinedAsGroupBases => groupSpecializationsAlreadyDefinedAsGroupBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers => duplicatelyDefinedGroupMembers;

    public IReadOnlyDictionary<NamedType, IVectorBaseType> DuplicatelyDefinedVectorBases => duplicatelyDefinedVectorBases;
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> DuplicatelyDefinedVectorSpecializations => duplicatelyDefinedVectorSpecializations;
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> VectorSpecializationsAlreadyDefinedAsVectorBases => vectorSpecializationsAlreadyDefinedAsVectorBases;

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorBaseType> vectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorType> vectors { get; }

    private ReadOnlyEquatableDictionary<NamedType, IGroupPopulation> groupMembersByGroup { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupBaseType> duplicatelyDefinedGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupSpecializationType> duplicatelyDefinedGroupSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorBaseType> duplicatelyDefinedVectorBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorSpecializationType> duplicatelyDefinedVectorSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases { get; }

    IReadOnlyDictionary<NamedType, IQuantityBaseType> IQuantityPopulation.QuantityBases => GroupBases.Transform(static (vector) => vector as IQuantityBaseType)
        .Concat(VectorBases.Transform(static (vector) => vector as IQuantityBaseType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Groups.Transform(static (vector) => vector as IQuantityType)
        .Concat(Vectors.Transform(static (vector) => vector as IQuantityType)).Concat(GroupMembers.Transform(static (vector) => vector as IQuantityType)).ToDictionary().AsEquatable();

    private VectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupBaseType> vectorGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroups,
        IReadOnlyDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers, IReadOnlyDictionary<NamedType, IVectorBaseType> vectorBases, IReadOnlyDictionary<NamedType, IVectorType> vectors,
        IReadOnlyDictionary<NamedType, IGroupPopulation> groupMembersByGroup, IReadOnlyDictionary<NamedType, IVectorGroupBaseType> duplicatelyDefinedGroupBases,
        IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> duplicatelyDefinedGroupSpecializations, IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases,
        IReadOnlyDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers, IReadOnlyDictionary<NamedType, IVectorBaseType> duplicatelyDefinedVectorBases,
        IReadOnlyDictionary<NamedType, IVectorSpecializationType> duplicatelyDefinedVectorSpecializations, IReadOnlyDictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases)
    {
        this.vectorGroupBases = vectorGroupBases.AsReadOnlyEquatable();
        this.vectorBases = vectorBases.AsReadOnlyEquatable();

        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();
        this.vectors = vectors.AsReadOnlyEquatable();

        this.groupMembersByGroup = groupMembersByGroup.AsReadOnlyEquatable();

        this.duplicatelyDefinedGroupBases = duplicatelyDefinedGroupBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedGroupSpecializations = duplicatelyDefinedGroupSpecializations.AsReadOnlyEquatable();
        this.groupSpecializationsAlreadyDefinedAsGroupBases = groupSpecializationsAlreadyDefinedAsGroupBases.AsReadOnlyEquatable();

        this.duplicatelyDefinedGroupMembers = duplicatelyDefinedGroupMembers.AsReadOnlyEquatable();

        this.duplicatelyDefinedVectorBases = duplicatelyDefinedVectorBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedVectorSpecializations = duplicatelyDefinedVectorSpecializations.AsReadOnlyEquatable();
        this.vectorSpecializationsAlreadyDefinedAsVectorBases = vectorSpecializationsAlreadyDefinedAsVectorBases.AsReadOnlyEquatable();
    }

    public static VectorPopulation Build(IReadOnlyList<IVectorBaseType> vectorBases, IReadOnlyList<IVectorSpecializationType> vectorSpecializations, IReadOnlyList<IVectorGroupBaseType> groupBases,
        IReadOnlyList<IVectorGroupSpecializationType> groupSpecializations, IReadOnlyList<IVectorGroupMemberType> groupMembers)
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

        var unassignedGroupSpecializations = groupSpecializationPopulation.Values.ToList();
        var unassignedVectorSpecializations = vectorSpecializationPopulation.Values.ToList();

        iterativelySetBaseGroupForSpecializations();
        iterativelySetBaseVectorForSpecializations();

        return new(groupBasePopulation, groupPopulation, groupMemberPopulation, vectorBasePopulation, vectorPopulation, groupMembersByGroup.Transform(static (group) => new GroupPopulation(group) as IGroupPopulation),
            duplicateGroupBasePopulation, duplicateGroupSpecializationPopulation, groupSpecializationsAlreadyDefinedAsGroupBases, duplicateGroupMemberPopulation, duplicateVectorBasePopulation, duplicateVectorSpecializationPopulation, vectorSpecializationsAlreadyDefinedAsVectorBases);

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
