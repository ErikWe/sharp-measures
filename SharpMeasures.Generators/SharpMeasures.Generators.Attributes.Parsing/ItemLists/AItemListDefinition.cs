namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListDefinition<TItem, TLocations> : AAttributeDefinition<TLocations>, IItemListDefinition<TItem, TLocations>
    where TLocations : IItemListLocations
{
    protected IReadOnlyList<TItem> Items => items;

    public IReadOnlyList<int> LocationMap => locationMap;

    private ReadOnlyEquatableList<TItem> items { get; } = ReadOnlyEquatableList<TItem>.Empty;
    IReadOnlyList<TItem> IItemListDefinition<TItem, TLocations>.Items => Items;

    private ReadOnlyEquatableList<int> locationMap { get; }

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations, IReadOnlyList<int> locationMap) : base(locations)
    {
        this.items = items.AsReadOnlyEquatable();

        this.locationMap = locationMap.AsReadOnlyEquatable();
    }

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations) : base(locations)
    {
        this.items = items.AsReadOnlyEquatable();

        var locationMap = new int[items.Count];

        for (int i = 0; i < locationMap.Length; i++)
        {
            locationMap[i] = i;
        }

        this.locationMap = locationMap.AsReadOnlyEquatable();
    }
}
