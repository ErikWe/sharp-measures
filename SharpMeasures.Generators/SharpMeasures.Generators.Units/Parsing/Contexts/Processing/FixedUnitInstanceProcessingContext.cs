namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using System.Collections.Generic;

internal record class FixedUnitInstanceProcessingContext : UnitInstanceProcessingContext, IFixedUnitInstanceProcessingContext
{
    public bool UnitIsDerivable { get; }

    public FixedUnitInstanceProcessingContext(DefinedType type, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms, bool unitIsDerivable) : base(type, reservedUnitInstanceNames, reservedUnitInstancePluralForms)
    {
        UnitIsDerivable = unitIsDerivable;
    }
}
