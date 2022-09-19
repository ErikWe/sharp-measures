namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public sealed record class ExcludeUnitsFilteringContext : IExcludeUnitsFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> IncludedUnitInstances { get; }

    public ExcludeUnitsFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> includedUnitInstances)
    {
        Type = type;

        UnitType = unitType;

        IncludedUnitInstances = includedUnitInstances;
    }
}
