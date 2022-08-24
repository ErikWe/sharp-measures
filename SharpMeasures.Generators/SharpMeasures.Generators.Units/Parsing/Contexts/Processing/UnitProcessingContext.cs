namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class UnitProcessingContext : SimpleProcessingContext, IUnitProcessingContext
{
    public HashSet<string> ReservedUnits { get; } = new();
    public HashSet<string> ReservedUnitPlurals { get; } = new();

    public UnitProcessingContext(DefinedType type) : base(type) { }
}
