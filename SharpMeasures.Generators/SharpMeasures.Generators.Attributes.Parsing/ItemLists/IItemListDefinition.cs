namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public interface IItemListDefinition<out TItem, out TLocations> : IAttributeDefinition<TLocations> where TLocations : IItemListLocations
{
    public abstract IReadOnlyList<TItem> Items { get; }
}

public interface IOpenItemListDefinition<TItem, out TDefinition, TLocations> : IItemListDefinition<TItem, TLocations>, IOpenAttributeDefinition<TDefinition, TLocations>
    where TDefinition : IOpenItemListDefinition<TItem, TDefinition, TLocations>
    where TLocations : IItemListLocations
{
    public abstract TDefinition WithItems(IReadOnlyList<TItem> items);
}
