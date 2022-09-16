namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class ScalarProcessingData
{
    public IReadOnlyDictionary<NamedType, IScalarBaseType> DuplicatelyDefinedScalarBases => duplicatelyDefinedScalarBases;
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> DuplicatelyDefinedScalarSpecializations => duplicatelyDefinedScalarSpecializations;
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> ScalarSpecializationsAlreadyDefinedAsScalarBases => scalarSpecializationsAlreadyDefinedAsScalarBases;

    private ReadOnlyEquatableDictionary<NamedType, IScalarBaseType> duplicatelyDefinedScalarBases { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarSpecializationType> duplicatelyDefinedScalarSpecializations { get; }
    private ReadOnlyEquatableDictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsScalarBases { get; }

    public ScalarProcessingData(IReadOnlyDictionary<NamedType, IScalarBaseType> duplicatelyDefinedScalarBases, IReadOnlyDictionary<NamedType, IScalarSpecializationType> duplicatelyDefinedScalarSpecializations,
        IReadOnlyDictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsScalarBases)
    {
        this.duplicatelyDefinedScalarBases = duplicatelyDefinedScalarBases.AsReadOnlyEquatable();
        this.duplicatelyDefinedScalarSpecializations = duplicatelyDefinedScalarSpecializations.AsReadOnlyEquatable();
        this.scalarSpecializationsAlreadyDefinedAsScalarBases = scalarSpecializationsAlreadyDefinedAsScalarBases.AsReadOnlyEquatable();
    }
}
