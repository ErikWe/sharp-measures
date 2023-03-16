namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityConstant;

using System.Collections.Generic;

public sealed record class QuantityConstantProcessingContext : IQuantityConstantProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<string> ReservedConstantNames { get; } = new();
    public HashSet<string> ReservedConstantMultiples { get; } = new();

    public QuantityConstantProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
