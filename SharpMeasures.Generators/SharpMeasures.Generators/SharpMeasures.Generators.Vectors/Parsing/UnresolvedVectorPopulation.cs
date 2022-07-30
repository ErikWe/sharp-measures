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
        Dictionary<NamedType, IUnresolvedVectorGroupType> vectorGroupPopulation = new(vectorGroupBases.Length + vectorGroupSpecializations.Length + individualVectorBases.Length + individualVectorSpecializations.Length);
        Dictionary<NamedType, IUnresolvedVectorGroupBaseType> vectorGroupBasePopulation = new(vectorGroupBases.Length + individualVectorBases.Length);

        Dictionary<NamedType, IUnresolvedIndividualVectorType> individualVectorPopulation = new(individualVectorBases.Length + individualVectorSpecializations.Length);
        Dictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBasePopulation = new(individualVectorBases.Length);

        Dictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMemberPopulation = new(vectorGroupMembers.Length);

        Dictionary<NamedType, IUnresolvedVectorGroupType> duplicatePopulation = new();
        Dictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicateVectorGroupMemberPopulation = new();

        foreach (var vectorGroup in (vectorGroupBases as IEnumerable<IUnresolvedVectorGroupType>).Concat(vectorGroupSpecializations).Concat(individualVectorBases).Concat(individualVectorSpecializations))
        {
            if (vectorGroupPopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup))
            {
                continue;
            }

            duplicatePopulation.TryAdd(vectorGroup.Type.AsNamedType(), vectorGroup);
        }

        foreach (var vectorGroupBase in vectorGroupBases.Concat(individualVectorBases))
        {
            vectorGroupBasePopulation.TryAdd(vectorGroupBase.Type.AsNamedType(), vectorGroupBase);
        }

        foreach (var individualVector in (individualVectorBases as IEnumerable<IUnresolvedIndividualVectorType>).Concat(individualVectorSpecializations))
        {
            individualVectorPopulation.TryAdd(individualVector.Type.AsNamedType(), individualVector);
        }

        foreach (var individualVectorBase in individualVectorBases)
        {
            individualVectorBasePopulation.TryAdd(individualVectorBase.Type.AsNamedType(), individualVectorBase);
        }

        foreach (var vectorGroupMember in vectorGroupMembers)
        {
            if (vectorGroupMemberPopulation.TryAdd(vectorGroupMember.Type.AsNamedType(), vectorGroupMember))
            {
                continue;
            }

            duplicateVectorGroupMemberPopulation.TryAdd(vectorGroupMember.Type.AsNamedType(), vectorGroupMember);
        }

        var unassignedVectorGroupSpecializations = vectorGroupSpecializations.Concat(individualVectorSpecializations).ToList();
        var unassignedIndividualVectorSpecializations = individualVectorSpecializations.ToList();

        iterativelySetBaseVectorGroupForSpecializations();
        iterativelySetBaseIndividualVectorForSpecializations();

        Dictionary<NamedType, IUnresolvedVectorGroupSpecializationType> unassignedSpecializationPopulation = new(unassignedVectorGroupSpecializations.Count + unassignedIndividualVectorSpecializations.Count);

        foreach (var unassignedSpecialization in unassignedVectorGroupSpecializations.Concat(unassignedIndividualVectorSpecializations))
        {
            unassignedSpecializationPopulation.TryAdd(unassignedSpecialization.Type.AsNamedType(), unassignedSpecialization);
        }

        return new(vectorGroupPopulation, vectorGroupBasePopulation, vectorGroupMemberPopulation, individualVectorPopulation, individualVectorBasePopulation, duplicatePopulation,
            duplicateVectorGroupMemberPopulation, unassignedSpecializationPopulation);

        void iterativelySetBaseVectorGroupForSpecializations()
        {
            int startLength = unassignedVectorGroupSpecializations.Count;

            for (int i = 0; i < unassignedVectorGroupSpecializations.Count; i++)
            {
                if (vectorGroupBasePopulation.TryGetValue(unassignedVectorGroupSpecializations[i].Definition.OriginalVectorGroup, out var vectorGroupBase))
                {
                    vectorGroupBasePopulation.TryAdd(unassignedVectorGroupSpecializations[i].Type.AsNamedType(), vectorGroupBase);

                    unassignedVectorGroupSpecializations.RemoveAt(i);
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
                }
            }

            if (startLength != unassignedIndividualVectorSpecializations.Count)
            {
                iterativelySetBaseIndividualVectorForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> VectorGroups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> VectorGroupBases => baseVectorGroupByVectorGroupType;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;

    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> IndividualVectors => individualVectors;
    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> IndividualVectorBases => individualVectorBases;

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> DuplicatelyDefined => duplicatelyDefined;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers => duplicatelyDefinedVectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupSpecializationType> UnassignedSpecializations => unassignedSpecializations;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities
        => VectorGroups.Transform(static (vector) => vector as IUnresolvedQuantityType)
        .Concat(IndividualVectors.Transform(static (vector) => vector as IUnresolvedQuantityType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityBaseType> IUnresolvedQuantityPopulation.QuantityBases
        => VectorGroupBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)
        .Concat(IndividualVectorBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)).ToDictionary().AsEquatable();

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupBaseType> baseVectorGroupByVectorGroupType { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorType> individualVectors { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupType> duplicatelyDefined { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupSpecializationType> unassignedSpecializations { get; }

    public UnresolvedVectorPopulation(IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> baseVectorGroupByVectorGroupType,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers, IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> individualVectors,
        IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> individualVectorBases, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> duplicatelyDefined,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> duplicatelyDefinedVectorGroupMembers, IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupSpecializationType> unassignedSpecializations)
    {
        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.baseVectorGroupByVectorGroupType = baseVectorGroupByVectorGroupType.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();

        this.individualVectors = individualVectors.AsReadOnlyEquatable();
        this.individualVectorBases = individualVectorBases.AsReadOnlyEquatable();

        this.duplicatelyDefined = duplicatelyDefined.AsReadOnlyEquatable();
        this.duplicatelyDefinedVectorGroupMembers = duplicatelyDefinedVectorGroupMembers.AsReadOnlyEquatable();
        this.unassignedSpecializations = unassignedSpecializations.AsReadOnlyEquatable();
    }
}
