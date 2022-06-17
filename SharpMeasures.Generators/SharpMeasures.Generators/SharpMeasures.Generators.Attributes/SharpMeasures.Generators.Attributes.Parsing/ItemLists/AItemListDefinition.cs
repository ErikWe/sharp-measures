namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListDefinition<TItem, TLocations> : AAttributeDefinition<TLocations>, IItemListDefinition<TItem>
    where TLocations : AItemListLocations
{
    public ReadOnlyEquatableList<TItem> Items { get; }
    IReadOnlyList<TItem> IItemListDefinition<TItem>.Items => Items;

    IItemListLocations IItemListDefinition<TItem>.Locations => Locations;

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations) : base(locations)
    {
        Items = items.AsReadOnlyEquatable();
    }
}
