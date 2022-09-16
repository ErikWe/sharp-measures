namespace SharpMeasures.Generators.Vectors.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class VectorProcessingData
{
    public IReadOnlyDictionary<NamedType, IVectorBaseType> DuplicatelyDefinedVectorBases => duplicatelyDefinedVectorBases;
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> DuplicatelyDefinedVectorSpecializations => duplicatelyDefinedVectorSpecializations;
    public IReadOnlyDictionary<NamedType, IVectorSpecializationType> VectorSpecializationsAlreadyDefinedAsVectorBases => vectorSpecializationsAlreadyDefinedAsVectorBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupBaseType> DuplicatelyDefinedGroupBases => duplicatelyDefinedGroupBases;
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> DuplicatelyDefinedGroupSpecializations => duplicatelyDefinedGroupSpecializations;
    public IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> GroupSpecializationsAlreadyDefinedAsGroupBases => groupSpecializationsAlreadyDefinedAsGroupBases;

    public IReadOnlyDictionary<NamedType, IVectorGroupMemberType> DuplicatelyDefinedGroupMembers => duplicatelyDefinedGroupMembers;

    private ReadOnlyEquatableDictionary<NamedType, IVectorBaseType> duplicatelyDefinedVectorBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorSpecializationType> duplicatelyDefinedVectorSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupBaseType> duplicatelyDefinedGroupBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupSpecializationType> duplicatelyDefinedGroupSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases { get; }

    private ReadOnlyEquatableDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers { get; }

    public VectorProcessingData(IReadOnlyDictionary<NamedType, IVectorBaseType> duplicatelyDefinedVectorBases, IReadOnlyDictionary<NamedType, IVectorSpecializationType> duplicatelyDefinedVectorSpecializations,
        IReadOnlyDictionary<NamedType, IVectorSpecializationType> vectorSpecializationsAlreadyDefinedAsVectorBases, IReadOnlyDictionary<NamedType, IVectorGroupBaseType> duplicatelyDefinedGroupBases,
        IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> duplicatelyDefinedGroupSpecializations, IReadOnlyDictionary<NamedType, IVectorGroupSpecializationType> groupSpecializationsAlreadyDefinedAsGroupBases,
        IReadOnlyDictionary<NamedType, IVectorGroupMemberType> duplicatelyDefinedGroupMembers)
    {
        this.duplicatelyDefinedVectorBases = duplicatelyDefinedVectorBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedVectorSpecializations = duplicatelyDefinedVectorSpecializations.AsReadOnlyEquatable();
        this.vectorSpecializationsAlreadyDefinedAsVectorBases = vectorSpecializationsAlreadyDefinedAsVectorBases.AsReadOnlyEquatable();

        this.duplicatelyDefinedGroupBases = duplicatelyDefinedGroupBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedGroupSpecializations = duplicatelyDefinedGroupSpecializations.AsReadOnlyEquatable();
        this.groupSpecializationsAlreadyDefinedAsGroupBases = groupSpecializationsAlreadyDefinedAsGroupBases.AsReadOnlyEquatable();

        this.duplicatelyDefinedGroupMembers = duplicatelyDefinedGroupMembers.AsReadOnlyEquatable();
    }
}
