namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public sealed record class IncludeUnitsFilteringContext : IIncludeUnitsFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public bool AllUnitInstancesIncluded { get; }
    public HashSet<string> IncludedUnitInstances { get; }

    public IncludeUnitsFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedUnitInstances)
    {
        Type = type;

        UnitType = unitType;

        AllUnitInstancesIncluded = unitType.UnitInstancesByName.Count == includedUnitInstances.Count;
        IncludedUnitInstances = includedUnitInstances;
    }
}
