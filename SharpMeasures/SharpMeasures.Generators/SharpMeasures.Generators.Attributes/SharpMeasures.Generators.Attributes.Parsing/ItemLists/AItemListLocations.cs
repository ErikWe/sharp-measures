namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class AItemListLocations : AAttributeLocations, IItemListLocations
{
    public MinimalLocation? ItemsCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ItemsElements { get; init; } = Array.Empty<MinimalLocation>();

    public bool ExplicitlySetItems => ItemsCollection is not null;

    public virtual bool Equals(AItemListLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && ItemsCollection == other.ItemsCollection && ItemsElements.SequenceEqual(other.ItemsElements);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ ItemsCollection.GetHashCode() ^ ItemsElements.GetSequenceHashCode();
    }
}
