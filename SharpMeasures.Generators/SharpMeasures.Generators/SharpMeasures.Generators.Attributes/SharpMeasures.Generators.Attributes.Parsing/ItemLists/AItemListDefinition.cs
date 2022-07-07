namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListDefinition<TItem, TLocations> : AAttributeDefinition<TLocations>, IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    protected IReadOnlyList<TItem> Items => items;

    private ReadOnlyEquatableList<TItem> items { get; } = ReadOnlyEquatableList<TItem>.Empty;
    IReadOnlyList<TItem> IItemListDefinition<TItem, TLocations>.Items => Items;

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations) : base(locations)
    {
        this.items = items.AsReadOnlyEquatable();
    }
}
