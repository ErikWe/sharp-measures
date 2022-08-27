namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public record class IncludeUnitsFilteringContext : IIncludeUnitsFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public bool AllUnitsIncluded { get; }
    public HashSet<string> IncludedUnits { get; }

    public IncludeUnitsFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedUnits)
    {
        Type = type;

        UnitType = unitType;

        AllUnitsIncluded = unitType.UnitsByName.Count == includedUnits.Count;
        IncludedUnits = includedUnits;
    }
}
