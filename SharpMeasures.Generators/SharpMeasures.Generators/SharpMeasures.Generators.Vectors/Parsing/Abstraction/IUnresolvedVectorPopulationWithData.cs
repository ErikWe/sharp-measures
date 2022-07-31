namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IUnresolvedVectorPopulationWithData : IUnresolvedVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> DuplicatelyDefinedVectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedIndividualVectorType> DuplicatelyDefinedIndividualVectors { get; }
}
