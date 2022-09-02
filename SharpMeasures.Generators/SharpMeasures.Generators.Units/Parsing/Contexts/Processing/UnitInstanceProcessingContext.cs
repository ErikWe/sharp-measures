namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class UnitInstanceProcessingContext : SimpleProcessingContext, IUnitInstanceProcessingContext
{
    public HashSet<string> ReservedUnitInstanceNames { get; }
    public HashSet<string> ReservedUnitInstancePluralForms { get; }

    public UnitInstanceProcessingContext(DefinedType type, HashSet<string> reservedUnitInstanceNames, HashSet<string> reservedUnitInstancePluralForms) : base(type)
    {
        ReservedUnitInstanceNames = reservedUnitInstanceNames;
        ReservedUnitInstancePluralForms = reservedUnitInstancePluralForms;
    }
}
