namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IItemListDefinition : IAttributeDefinition
{
    public abstract IReadOnlyList<string> ItemNames { get; }

    new public abstract IItemListParsingData ParsingData { get; }
}
