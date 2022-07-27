namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

internal class UnresolvedVectorPopulation : IUnresolvedVectorPopulation
{
    public static UnresolvedVectorPopulation Build(ImmutableArray<IUnresolvedVectorGroupBaseType> baseVectorGroups,
        ImmutableArray<IUnresolvedVectorGroupSpecializationType> specializedVectorGroups, ImmutableArray<IUnresolvedVectorGroupMemberType> vectorGroupMembers,
        ImmutableArray<IUnresolvedIndividualVectorBaseType> baseIndividualVectors, ImmutableArray<IUnresolvedIndividualVectorSpecializationType> specializedIndividualVectors)
    {
        var vectorGroups = (baseVectorGroups as IEnumerable<IUnresolvedVectorGroupType>).Concat(specializedVectorGroups).Concat(baseIndividualVectors)
            .Concat(specializedIndividualVectors).ToDictionary(static (vectorGroup) => vectorGroup.Type.AsNamedType());

        var baseVectorGroupByVectorGroupType = baseVectorGroups.Concat(baseIndividualVectors).ToDictionary(static (baseVectorGroup) => baseVectorGroup.Type.AsNamedType());

        var unassignedSpecializedVectorGroups = specializedVectorGroups.Concat(specializedIndividualVectors).ToList();

        var individualVectors = (baseIndividualVectors as IEnumerable<IUnresolvedIndividualVectorType>).Concat(specializedIndividualVectors)
            .ToDictionary(static (vector) => vector.Type.AsNamedType());

        var baseIndividualVectorByIndividualVectorType = baseIndividualVectors.ToDictionary(static (baseVector) => baseVector.Type.AsNamedType());

        var unassignedSpecializedIndividualVectors = specializedIndividualVectors.ToList();

        iterativelySetBaseVectorGroupForSpecializations();
        iterativelySetBaseIndividualVectorForSpecializations();

        var vectorGroupMembersDictionary = vectorGroupMembers.ToDictionary(static (member) => member.Type.AsNamedType());

        return new(vectorGroups, baseVectorGroupByVectorGroupType, vectorGroupMembersDictionary, individualVectors, baseIndividualVectorByIndividualVectorType);

        void iterativelySetBaseVectorGroupForSpecializations()
        {
            int startLength = unassignedSpecializedVectorGroups.Count;

            for (int i = 0; i < unassignedSpecializedVectorGroups.Count; i++)
            {
                if (baseVectorGroupByVectorGroupType.TryGetValue(unassignedSpecializedVectorGroups[i].Definition.OriginalVectorGroup, out var baseVectorGroup))
                {
                    unassignedSpecializedVectorGroups.RemoveAt(i);

                    baseVectorGroupByVectorGroupType[unassignedSpecializedVectorGroups[i].Type.AsNamedType()] = baseVectorGroup;
                }
            }

            if (startLength != unassignedSpecializedVectorGroups.Count)
            {
                iterativelySetBaseIndividualVectorForSpecializations();
            }
        }

        void iterativelySetBaseIndividualVectorForSpecializations()
        {
            int startLength = unassignedSpecializedIndividualVectors.Count;

            for (int i = 0; i < unassignedSpecializedIndividualVectors.Count; i++)
            {
                if (baseIndividualVectorByIndividualVectorType.TryGetValue(unassignedSpecializedIndividualVectors[i].Definition.OriginalVector, out var baseIndividualVector))
                {
                    unassignedSpecializedIndividualVectors.RemoveAt(i);

                    baseIndividualVectorByIndividualVectorType[unassignedSpecializedIndividualVectors[i].Type.AsNamedType()] = baseIndividualVector;
                }
            }

            if (startLength != unassignedSpecializedIndividualVectors.Count)
            {
                iterativelySetBaseIndividualVectorForSpecializations();
            }
        }
    }

    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> VectorGroups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> VectorGroupBases => baseVectorGroupByVectorGroupType;
    public IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;

    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> IndividualVectors => vectors;
    public IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> IndividualVectorBases => baseVectorByVectorType;

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityType> IUnresolvedQuantityPopulation.Quantities
        => VectorGroups.Transform(static (vector) => vector as IUnresolvedQuantityType)
        .Concat(IndividualVectors.Transform(static (vector) => vector as IUnresolvedQuantityType)).ToDictionary().AsEquatable();

    IReadOnlyDictionary<NamedType, IUnresolvedQuantityBaseType> IUnresolvedQuantityPopulation.QuantityBases
        => VectorGroupBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)
        .Concat(IndividualVectorBases.Transform(static (vector) => vector as IUnresolvedQuantityBaseType)).ToDictionary().AsEquatable();

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupBaseType> baseVectorGroupByVectorGroupType { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers { get; }

    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorType> vectors { get; }
    private ReadOnlyEquatableDictionary<NamedType, IUnresolvedIndividualVectorBaseType> baseVectorByVectorType { get; }

    public UnresolvedVectorPopulation(IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> vectorGroups,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> baseVectorGroupByVectorGroupType,
        IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> vectorGroupMembers,
        IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> individualVectors,
        IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> baseIndividualVectorByIndividualVectorType)
    {
        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.baseVectorGroupByVectorGroupType = baseVectorGroupByVectorGroupType.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();

        this.vectors = individualVectors.AsReadOnlyEquatable();
        this.baseVectorByVectorType = baseIndividualVectorByIndividualVectorType.AsReadOnlyEquatable();
    }
}
