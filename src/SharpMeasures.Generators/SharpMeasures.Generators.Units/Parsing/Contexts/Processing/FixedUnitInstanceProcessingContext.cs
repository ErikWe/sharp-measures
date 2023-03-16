namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using System.Collections.Generic;

internal sealed record class FixedUnitInstanceProcessingContext : IFixedUnitInstanceProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<string> ReservedUnitInstanceNames { get; }
    public HashSet<string> ReservedUnitInstancePluralForms { get; }

    public bool UnitIsDerivable { get; }

    public FixedUnitInstanceProcessingContext(DefinedType type, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms, bool unitIsDerivable)
    {
        Type = type;

        ReservedUnitInstanceNames = reservedUnitInstanceNames;
        ReservedUnitInstancePluralForms = reservedUnitInstancePluralForms;

        UnitIsDerivable = unitIsDerivable;
    }
}
