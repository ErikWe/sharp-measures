namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class ARawItemListDefinition<TItem, TDefinition, TLocations> : ARawAttributeDefinition<TDefinition, TLocations>,
    IOpenItemListDefinition<TItem, TDefinition, TLocations>
    where TDefinition : ARawItemListDefinition<TItem, TDefinition, TLocations>
    where TLocations : IItemListLocations
{
    protected IReadOnlyList<TItem> Items
    {
        get => items;
        private init => items = value.AsReadOnlyEquatable();
    }

    IReadOnlyList<TItem> IItemListDefinition<TItem, TLocations>.Items => Items;
    private ReadOnlyEquatableList<TItem> items { get; init; } = ReadOnlyEquatableList<TItem>.Empty;

    protected ARawItemListDefinition(TLocations locations) : base(locations) { }

    protected TDefinition WithItems(IReadOnlyList<TItem> items) => Definition with { Items = items };

    TDefinition IOpenItemListDefinition<TItem, TDefinition, TLocations>.WithItems(IReadOnlyList<TItem> items) => WithItems(items);
}
