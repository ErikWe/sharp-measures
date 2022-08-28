namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Units.Parsing.FixedUnit;

using System.Collections.Generic;

internal record class FixedUnitProcessingContext : UnitProcessingContext, IFixedUnitProcessingContext
{
    public bool UnitIsDerivable { get; }

    public FixedUnitProcessingContext(DefinedType type, HashSet<string> reservedUnits, HashSet<string> reservedUnitPlurals, bool unitIsDerivable) : base(type, reservedUnits, reservedUnitPlurals)
    {
        UnitIsDerivable = unitIsDerivable;
    }
}
