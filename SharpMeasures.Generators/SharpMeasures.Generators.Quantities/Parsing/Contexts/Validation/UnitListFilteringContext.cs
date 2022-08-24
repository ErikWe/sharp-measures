namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Validation;

using SharpMeasures.Generators.Quantities.Parsing.UnitList;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public record class UnitListFilteringContext : IUnitListFilteringContext
{
    public DefinedType Type { get; }

    public IUnitType UnitType { get; }

    public HashSet<string> InheritedUnits { get; }

    public UnitListFilteringContext(DefinedType type, IUnitType unitType, HashSet<string> inheritedUnits)
    {
        Type = type;

        UnitType = unitType;

        InheritedUnits = inheritedUnits;
    }
}
