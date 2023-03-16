namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal sealed record class UnitInstanceProcessingContext : IUnitInstanceProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<string> ReservedUnitInstanceNames { get; }
    public HashSet<string> ReservedUnitInstancePluralForms { get; }

    public UnitInstanceProcessingContext(DefinedType type, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms)
    {
        Type = type;

        ReservedUnitInstanceNames = reservedUnitInstanceNames;
        ReservedUnitInstancePluralForms = reservedUnitInstancePluralForms;
    }
}
