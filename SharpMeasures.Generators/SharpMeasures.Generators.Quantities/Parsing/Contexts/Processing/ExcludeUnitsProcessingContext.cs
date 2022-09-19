namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class ExcludeUnitsProcessingContext : IUniqueItemListProcessingContext<string>
{
    public DefinedType Type { get; }

    public HashSet<string> ListedItems { get; } = new();

    public ExcludeUnitsProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
