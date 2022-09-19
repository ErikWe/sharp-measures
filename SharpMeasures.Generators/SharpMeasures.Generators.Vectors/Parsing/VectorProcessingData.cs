namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class VectorProcessingData
{
    public IReadOnlyDictionary<NamedType, IVectorBaseType> DuplicatelyDefinedVectorBases { get; }
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> DuplicatelyDefinedVectorSpecializations { get; }
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> VectorSpecializationsAlreadyDefinedAsVectorBases { get; }

    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> DuplicatelyDefinedGroupBases { get; }
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> DuplicatelyDefinedGroupSpecializations { get; }
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> GroupSpecializationsAlreadyDefinedAsGroupBases { get; }

    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers { get; }

    public VectorProcessingData(IReadOnlyDictionary<NamedType, IVectorBaseType> duplicatelyDefinedVectorBases, IReadOnlyDictionary<NamedType, IVectorSpecializationType> duplicatelyDefinedVectorSpecializations, IReadOnlyDictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases,
        IReadOnlyDictionary<NamedType, IVectorGroupBaseType> duplicatelyDefinedGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> duplicatelyDefinedGroupSpecializations, IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases, IReadOnlyDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers)
    {
        DuplicatelyDefinedVectorBases = duplicatelyDefinedVectorBases.AsReadOnlyEquatable();
        DuplicatelyDefinedVectorSpecializations = duplicatelyDefinedVectorSpecializations.AsReadOnlyEquatable();
        VectorSpecializationsAlreadyDefinedAsVectorBases = vectorSpecializationsAlreadyDefinedAsVectorBases.AsReadOnlyEquatable();

        DuplicatelyDefinedGroupBases = duplicatelyDefinedGroupBases.AsReadOnlyEquatable();
        DuplicatelyDefinedGroupSpecializations = duplicatelyDefinedGroupSpecializations.AsReadOnlyEquatable();
        GroupSpecializationsAlreadyDefinedAsGroupBases = groupSpecializationsAlreadyDefinedAsGroupBases.AsReadOnlyEquatable();

        DuplicatelyDefinedGroupMembers = duplicatelyDefinedGroupMembers.AsReadOnlyEquatable();
    }
}
