namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class AItemListDefinition<TItem, TLocations> : AAttributeDefinition<TLocations>, IItemListDefinition<TItem>
    where TLocations : AItemListLocations
{
    public IReadOnlyList<TItem> Items { get; }

    IItemListLocations IItemListDefinition<TItem>.Locations => Locations;

    protected AItemListDefinition(IReadOnlyList<TItem> items, TLocations locations) : base(locations)
    {
        Items = items;
    }

    public virtual bool Equals(ARawItemListDefinition<TItem, TLocations> other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Items.SequenceEqual(other.Items);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Items.GetSequenceHashCode();
    }
}
