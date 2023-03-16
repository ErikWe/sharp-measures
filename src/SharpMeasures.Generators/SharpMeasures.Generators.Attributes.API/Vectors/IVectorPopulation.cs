namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IVectorPopulation : IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupType> Groups { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupBaseType> GroupBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupMemberType> GroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IGroupPopulation> GroupMembersByGroup { get; }

    public abstract IReadOnlyDictionary<NamedType, IVectorType> Vectors { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorBaseType> VectorBases { get; }
}

public interface IGroupPopulation
{
    public abstract IReadOnlyDictionary<int, IVectorGroupMemberType> GroupMembersByDimension { get; }
}
