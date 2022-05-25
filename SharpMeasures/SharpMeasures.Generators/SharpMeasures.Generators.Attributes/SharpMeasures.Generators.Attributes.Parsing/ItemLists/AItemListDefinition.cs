namespace SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class AItemListDefinition<TParsingData, TLocations> : AAttributeDefinition<TParsingData, TLocations>, IItemListDefinition
    where TParsingData : AItemListParsingData<TLocations>
    where TLocations : AItemListLocations
{
    public IReadOnlyList<string> ItemNames { get; init; } = Array.Empty<string>();

    IItemListParsingData IItemListDefinition.ParsingData => ParsingData;

    protected AItemListDefinition(TParsingData parsingData) : base(parsingData) { }

    public virtual bool Equals(AItemListDefinition<TParsingData, TLocations> other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && ItemNames.SequenceEqual(other.ItemNames);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ ItemNames.GetSequenceHashCode();
    }
}
