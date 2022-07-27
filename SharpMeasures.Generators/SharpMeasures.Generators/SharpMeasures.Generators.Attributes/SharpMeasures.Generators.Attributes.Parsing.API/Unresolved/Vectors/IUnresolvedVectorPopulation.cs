namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

using System.Collections.Generic;

public interface IUnresolvedVectorPopulation : IUnresolvedQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> VectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupBaseType> VectorGroupBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> VectorGroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> IndividualVectors { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorBaseType> IndividualVectorBases { get; }
}
