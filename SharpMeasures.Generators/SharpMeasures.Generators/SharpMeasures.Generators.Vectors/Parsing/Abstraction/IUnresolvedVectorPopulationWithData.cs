namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using System.Collections.Generic;

internal interface IUnresolvedVectorPopulationWithData : IRawVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupType> DuplicatelyDefinedVectorGroups { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers { get; }
    public abstract IReadOnlyDictionary<NamedType, IRawVectorType> DuplicatelyDefinedIndividualVectors { get; }
}
