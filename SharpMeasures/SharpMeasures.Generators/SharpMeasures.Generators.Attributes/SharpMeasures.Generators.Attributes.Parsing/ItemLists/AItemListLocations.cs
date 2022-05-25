namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class AItemListLocations : AAttributeLocations, IItemListLocations
{
    public MinimalLocation ItemNamesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> ItemNamesElements { get; init; } = Array.Empty<MinimalLocation>();

    public virtual bool Equals(AItemListLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && ItemNamesCollection == other.ItemNamesCollection && ItemNamesElements.SequenceEqual(other.ItemNamesElements);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ ItemNamesCollection.GetHashCode() ^ ItemNamesElements.GetSequenceHashCode();
    }
}
