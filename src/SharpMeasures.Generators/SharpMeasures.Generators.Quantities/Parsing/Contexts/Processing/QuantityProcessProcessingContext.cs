namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityProcess;

using System.Collections.Generic;

public sealed record class QuantityProcessProcessingContext : IQuantityProcessProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<string> ReservedNames { get; } = new();
    public HashSet<(string, IReadOnlyList<NamedType>)> ReservedMethodSignatures { get; } = new();

    public QuantityProcessProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
