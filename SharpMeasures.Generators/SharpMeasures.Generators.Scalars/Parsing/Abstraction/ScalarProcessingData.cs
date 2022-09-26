namespace SharpMeasures.Generators.Scalars.Parsing.Abstraction;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class ScalarProcessingData
{
    public static ScalarProcessingData Empty { get; } = new(new Dictionary<NamedType, IScalarBaseType>(), new Dictionary<NamedType, IScalarSpecializationType>(), new Dictionary<NamedType, IScalarSpecializationType>());

    public IReadOnlyDictionary<NamedType, IScalarBaseType> DuplicatelyDefinedScalarBases { get; }
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> DuplicatelyDefinedScalarSpecializations { get; }
    public IReadOnlyDictionary<NamedType, IScalarSpecializationType> ScalarSpecializationsAlreadyDefinedAsScalarBases { get; }

    public ScalarProcessingData(IReadOnlyDictionary<NamedType, IScalarBaseType> duplicatelyDefinedScalarBases, IReadOnlyDictionary<NamedType, IScalarSpecializationType> duplicatelyDefinedScalarSpecializations, IReadOnlyDictionary<NamedType, IScalarSpecializationType> scalarSpecializationsAlreadyDefinedAsScalarBases)
    {
        DuplicatelyDefinedScalarBases = duplicatelyDefinedScalarBases.AsReadOnlyEquatable();
        DuplicatelyDefinedScalarSpecializations = duplicatelyDefinedScalarSpecializations.AsReadOnlyEquatable();
        ScalarSpecializationsAlreadyDefinedAsScalarBases = scalarSpecializationsAlreadyDefinedAsScalarBases.AsReadOnlyEquatable();
    }
}
