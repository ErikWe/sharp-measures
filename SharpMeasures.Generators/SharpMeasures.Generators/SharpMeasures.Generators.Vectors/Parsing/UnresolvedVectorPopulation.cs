namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedVectorPopulation : IUnresolvedVectorPopulationWithData
{
    public static UnresolvedVectorPopulation Build(ImmutableArray<IRawVectorGroupBaseType> vectorGroupBases,
        ImmutableArray<IRawVectorGroupSpecializationType> vectorGroupSpecializations, ImmutableArray<IRawVectorGroupMemberType> vectorGroupMembers,
        ImmutableArray<IRawVectorBaseType> individualVectorBases, ImmutableArray<IRawVectorSpecializationType> individualVectorSpecializations)
    {
        Dictionary<NamedType, IRawVectorGroupType> vectorGroupPopulation = new(vectorGroupBases.Length + vectorGroupSpecializations.Length);
        Dictionary<NamedType, IRawVectorGroupBaseType> vectorGroupBasePopulation = new(vectorGroupBases.Length + individualVectorBases.Length);
        Dictionary<NamedType, IRawVectorGroupMemberType> vectorGroupMemberPopulation = new(vectorGroupMembers.Length);

        Dictionary<NamedType, Dictionary<int, IRawVectorGroupMemberType>> vectorGroupMembersByGroup = new(vectorGroupBases.Length + vectorGroupSpecializations.Length);

        Dictionary<NamedType, IRawVectorType> individualVectorPopulation = new(individualVectorBases.Length);
        Dictionary<NamedType, IRawVectorBaseType> individualVectorBasePopulation = new(individualVectorBases.Length);

        Dictionary<NamedType, IRawVectorGroupType> duplicateVectorGroupPopulation = new();
        Dictionary<NamedType, IRawVectorGroupMemberType> duplicateVectorGroupMemberPopulation = new();
        Dictionary<NamedType, IRawVectorType> duplicateIndividualVectorPopulation = new();

        foreach (var vectorGroup in (vectorGroupBases as IEnumerable<IRawVectorGroupType>).Concat(vectorGroupSpecializations))
        {
            if (vectorGroupPopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup) is false)
            {
                duplicateVectorGroupPopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup);
            }

            vectorGroupMembersByGroup.TryAdd(vectorGroup.Type.AsNamedType(), new Dictionary<int, IRawVectorGroupMemberType>());
        }

        foreach (var vectorGroupBase in vectorGroupBases)
        {
            vectorGroupBasePopulation.TryAdd(vectorGroupBase.Type.AsNamedType(), vectorGroupBase);
        }

        foreach (var individualVector in (individualVectorBases as IEnumerable<IRawVectorType>).Concat(individualVectorSpecializations))
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
            vectorGroupMembersByGroup.Transform(static (members) => new UnresolvedVectorGroupPopulation(members) as IRawVectorGroupPopulation),
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

    public IReadOnlyDictionary<NamedType, IRawVectorGroupType> VectorGroups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IRawVectorGroupBaseType> VectorGroupBases => vectorGroupBases;
    public IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;

    public IReadOnlyDictionary<NamedType, IRawVectorGroupPopulation> VectorGroupMembersByGroup => vectorGroupMembersByGroup;

    public IReadOnlyDictionary<NamedType, IRawVectorType> IndividualVectors => individualVectors;
    public IReadOnlyDictionary<NamedType, IRawVectorBaseType> IndividualVectorBases => individualVectorBases;

    public IReadOnlyDictionary<NamedType, IRawVectorGroupType> DuplicatelyDefinedVectorGroups => duplicatelyDefinedVectorGroups;
    public IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers => duplicatelyDefinedVectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IRawVectorType> DuplicatelyDefinedIndividualVectors => duplicatelyDefinedIndividualVectors;

    IReadOnlyDictionary<NamedType, IRawQuantityType> IRawQuantityPopulation.Quantities => VectorGroups.Transform(static (vector) => vector as IRawQuantityType)
        .Concat(IndividualVectors.Transform(static (vector) => vector as IRawQuantityType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IRawQuantityBaseType> IRawQuantityPopulation.QuantityBases => VectorGroupBases.Transform(static (vector) => vector as IRawQuantityBaseType)
        .Concat(IndividualVectorBases.Transform(static (vector) => vector as IRawQuantityBaseType)).ToDictionary().AsEquatable();

    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupBaseType> vectorGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupMemberType> vectorGroupMembers { get; }

    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupPopulation> vectorGroupMembersByGroup { get; }

    private ReadOnlyEquatableDictionary<NamedType, IRawVectorType> individualVectors { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawVectorBaseType> individualVectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupType> duplicatelyDefinedVectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IRawVectorType> duplicatelyDefinedIndividualVectors { get; }

    public UnresolvedVectorPopulation(IReadOnlyDictionary<NamedType, IRawVectorGroupType> vectorGroups, IReadOnlyDictionary<NamedType, IRawVectorGroupBaseType> vectorGroupBases,
        IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> vectorGroupMembers, IReadOnlyDictionary<NamedType, IRawVectorGroupPopulation> vectorGroupMembersByGroup,
        IReadOnlyDictionary<NamedType, IRawVectorType> individualVectors, IReadOnlyDictionary<NamedType, IRawVectorBaseType> individualVectorBases,
        IReadOnlyDictionary<NamedType, IRawVectorGroupType> duplicatelyDefinedVectorGroups, IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers,
        IReadOnlyDictionary<NamedType, IRawVectorType> duplicatelyDefinedIndividualVectors)
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
