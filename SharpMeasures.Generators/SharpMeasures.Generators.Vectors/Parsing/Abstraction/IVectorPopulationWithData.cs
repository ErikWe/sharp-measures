namespace SharpMeasures.Generators.Vectors.Parsing.Abstraction;

using System.Collections.Generic;

internal interface IVectorPopulationWithData : IVectorPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IVectorBaseType> DuplicatelyDefinedVectorBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorSpecializationType> DuplicatelyDefinedVectorSpecializations { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorSpecializationType> VectorSpecializationsAlreadyDefinedAsVectorBases { get; }

    public abstract IReadOnlyDictionary<NamedType, IVectorGroupBaseType> DuplicatelyDefinedGroupBases { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> DuplicatelyDefinedGroupSpecializations { get; }
    public abstract IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> GroupSpecializationsAlreadyDefinedAsGroupBases { get; }

    public abstract IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers { get; }
}
