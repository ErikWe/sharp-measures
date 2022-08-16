namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;
using SharpMeasures.Generators.Raw.Vectors.Groups;

using System.Collections.Generic;

public interface IRawVectorPopulation : IRawQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupType> VectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupBaseType> VectorGroupBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> VectorGroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupPopulation> VectorGroupMembersByGroup { get; }

    public abstract IReadOnlyDictionary<NamedType, IRawVectorType> IndividualVectors { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawVectorBaseType> IndividualVectorBases { get; }
}

public interface IRawVectorGroupPopulation
{
    public abstract IReadOnlyDictionary<int, IRawVectorGroupMemberType> VectorGroupMembersByDimension { get; }
}
