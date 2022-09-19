namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using System.Collections.Generic;

public sealed record class ConvertibleQuantityProcessingContext : IConvertibleQuantityProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<NamedType> ListedQuantities { get; } = new();

    public ConvertibleQuantityProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
