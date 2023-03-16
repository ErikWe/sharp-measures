namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class IncludeUnitsProcessingContext : IUniqueItemListProcessingContext<string>
{
    public DefinedType Type { get; }

    public HashSet<string> ListedItems { get; } = new();

    public IncludeUnitsProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
