namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorPopulation : IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupMemberType> VectorGroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IIndividualVectorType> IndividualVectors { get; }
}
