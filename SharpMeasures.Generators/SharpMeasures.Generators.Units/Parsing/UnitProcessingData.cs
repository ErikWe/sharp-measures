namespace SharpMeasures.Generators.Units.Parsing;

using SharpMeasures.Equatables;

using System.Collections.Generic;

internal record class UnitProcessingData
{
    public IReadOnlyDictionary<NamedType, IUnitType> DuplicatelyDefinedUnits => duplicatelyDefinedUnits;

    private ReadOnlyEquatableDictionary<NamedType, IUnitType> duplicatelyDefinedUnits { get; }

    public UnitProcessingData(IReadOnlyDictionary<NamedType, IUnitType> duplicatelyDefinedUnits)
    {
        this.duplicatelyDefinedUnits = duplicatelyDefinedUnits.AsReadOnlyEquatable();
    }
}
