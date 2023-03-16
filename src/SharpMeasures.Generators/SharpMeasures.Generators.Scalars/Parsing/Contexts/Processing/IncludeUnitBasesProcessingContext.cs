namespace SharpMeasures.Generators.Scalars.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class IncludeUnitBasesProcessingContext : IUniqueItemListProcessingContext<string>
{
    public DefinedType Type { get; }

    public HashSet<string> ListedItems { get; } = new();

    public IncludeUnitBasesProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
