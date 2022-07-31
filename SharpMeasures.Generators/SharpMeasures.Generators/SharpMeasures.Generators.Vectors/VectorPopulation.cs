namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;
using System.Linq;

internal class VectorPopulation : IVectorPopulation
{
    public IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroups => vectorGroups;
    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> VectorGroupMembers => vectorGroupMembers;
    public IReadOnlyDictionary<NamedType, IIndividualVectorType> IndividualVectors => individualVectors;

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupType> vectorGroups { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers { get; }
    private ReadOnlyEquatableDictionary<NamedType, IIndividualVectorType> individualVectors { get; }

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => VectorGroups.Transform(static (vector) => vector as IQuantityType)
        .Concat(IndividualVectors.Transform(static (vector) => vector as IQuantityType)).ToDictionary().AsEquatable();

    public VectorPopulation(IReadOnlyDictionary<NamedType, IVectorGroupType> vectorGroups,
        IReadOnlyDictionary<NamedType, IVectorGroupMemberType> vectorGroupMembers,
        IReadOnlyDictionary<NamedType, IIndividualVectorType> individualVectors)
    {
        this.vectorGroups = vectorGroups.AsReadOnlyEquatable();
        this.vectorGroupMembers = vectorGroupMembers.AsReadOnlyEquatable();
        this.individualVectors = individualVectors.AsReadOnlyEquatable();
    }
}
