namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.ProcessedQuantity;

using System.Collections.Generic;

public sealed record class ProcessedQuantityProcessingContext : IProcessedQuantityProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<string> ReservedNames { get; } = new();

    public ProcessedQuantityProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
