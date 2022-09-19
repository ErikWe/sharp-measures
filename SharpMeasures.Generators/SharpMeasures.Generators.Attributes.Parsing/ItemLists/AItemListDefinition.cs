namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using SharpMeasures.Equatables;

using System.Collections.Generic;

public abstract record class AItemListDefinition<TItem, TLocations> : AAttributeDefinition<TLocations>, IItemListDefinition<TItem, TLocations> where TLocations : IItemListLocations
{
    protected IReadOnlyList<TItem> Items { get; } = ReadOnlyEquatableList<TItem>.Empty;

    public IReadOnlyList<int> LocationMap { get; }

    IReadOnlyList<TItem> IItemListDefinition<TItem, TLocations>.Items => Items;

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations, IReadOnlyList<int> locationMap) : base(locations)
    {
        Items = items.AsReadOnlyEquatable();

        LocationMap = locationMap.AsReadOnlyEquatable();
    }

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations) : base(locations)
    {
        Items = items.AsReadOnlyEquatable();

        var locationMap = new int[items.Count];

        for (int i = 0; i < locationMap.Length; i++)
        {
            locationMap[i] = i;
        }

        LocationMap = locationMap.AsReadOnlyEquatable();
    }
}
