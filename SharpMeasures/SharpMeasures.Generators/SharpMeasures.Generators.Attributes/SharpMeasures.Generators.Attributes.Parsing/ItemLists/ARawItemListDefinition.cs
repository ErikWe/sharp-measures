namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class ARawItemListDefinition<TItem, TLocations> : ARawAttributeDefinition<TLocations>, IItemListDefinition<TItem>
    where TLocations : AItemListLocations
{
    public ReadOnlyEquatableList<TItem> Items { get; init; } = ReadOnlyEquatableList<TItem>.Empty;
    IReadOnlyList<TItem> IItemListDefinition<TItem>.Items => Items;

    IItemListLocations IItemListDefinition<TItem>.Locations => Locations;

    protected ARawItemListDefinition(TLocations locations) : base(locations) { }
}
