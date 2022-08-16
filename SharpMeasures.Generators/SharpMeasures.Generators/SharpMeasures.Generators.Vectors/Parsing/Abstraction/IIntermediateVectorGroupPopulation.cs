namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Vectors.Groups;
using System.Collections.Generic;

internal interface IIntermediateVectorGroupPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupType> VectorGroupBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IIntermediateVectorGroupSpecializationType> VectorGroupSpecializations { get; }
    public abstract IReadOnlyDictionary<NamedType, IIntermediateVectorGroupMemberType> VectorGroupMembers { get; }
}
