namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class ARawItemListDefinition<TItem, TLocations> : ARawAttributeDefinition<TLocations>, IItemListDefinition<TItem>
    where TLocations : AItemListLocations
{
    public IReadOnlyList<TItem> Items { get; init; } = Array.Empty<TItem>();

    IItemListLocations IItemListDefinition<TItem>.Locations => Locations;

    protected ARawItemListDefinition(TLocations locations) : base(locations) { }

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
