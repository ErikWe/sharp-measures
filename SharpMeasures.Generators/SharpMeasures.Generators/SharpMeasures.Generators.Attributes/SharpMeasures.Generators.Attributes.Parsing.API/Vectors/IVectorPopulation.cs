namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Groups;

using System.Collections.Generic;

public interface IVectorPopulation : IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupMemberType> VectorGroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IVectorType> IndividualVectors { get; }
}
