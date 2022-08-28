namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class UnitProcessingContext : SimpleProcessingContext, IUnitProcessingContext
{
    public HashSet<string> ReservedUnits { get; }
    public HashSet<string> ReservedUnitPlurals { get; }

    public UnitProcessingContext(DefinedType type, HashSet<string> reservedUnits, HashSet<string> reservedUnitPlurals) : base(type)
    {
        ReservedUnits = reservedUnits;
        ReservedUnitPlurals = reservedUnitPlurals;
    }
}
