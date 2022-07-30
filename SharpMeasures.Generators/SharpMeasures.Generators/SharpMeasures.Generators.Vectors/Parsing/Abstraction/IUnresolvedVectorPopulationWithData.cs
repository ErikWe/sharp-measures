namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using SharpMeasures.Generators.Unresolved.Vectors;

using System.Collections.Generic;

internal interface IUnresolvedVectorPopulationWithData : IUnresolvedVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupType> DuplicatelyDefined { get; }
    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupMemberType> DuplicatelyDefinedVectorGroupMembers { get; }

    public abstract IReadOnlyDictionary<NamedType, IUnresolvedVectorGroupSpecializationType> UnassignedSpecializations { get; }
}
