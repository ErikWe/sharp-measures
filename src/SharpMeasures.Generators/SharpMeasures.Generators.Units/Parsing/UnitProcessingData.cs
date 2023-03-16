namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal sealed record class UnitProcessingData
{
    public static UnitProcessingData Empty { get; } = new(new Dictionary<NamedType, IUnitType>());

    public IReadOnlyDictionary<NamedType, IUnitType> DuplicatelyDefinedUnits { get; }

    public UnitProcessingData(IReadOnlyDictionary<NamedType, IUnitType> duplicatelyDefinedUnits)
    {
        DuplicatelyDefinedUnits = duplicatelyDefinedUnits.AsReadOnlyEquatable();
    }
}
