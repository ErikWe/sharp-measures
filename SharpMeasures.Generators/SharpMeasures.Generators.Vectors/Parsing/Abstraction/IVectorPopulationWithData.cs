namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IVectorPopulationWithData : IVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorType> DuplicatelyDefinedVectors { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupType> DuplicatelyDefinedGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers { get; }
}
