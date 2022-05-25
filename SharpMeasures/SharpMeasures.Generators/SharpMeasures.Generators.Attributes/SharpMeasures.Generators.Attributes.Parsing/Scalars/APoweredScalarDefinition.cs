namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class APoweredScalarDefinition<TParsingData, TLocations> : AAttributeDefinition<TParsingData, TLocations>, IPoweredScalarDefinition
    where TParsingData : APoweredScalarParsingData<TLocations>
    where TLocations : APoweredScalarLocations
{
    public NamedType Quantity { get; init; }
    public IReadOnlyList<NamedType> SecondaryQuantities { get; init; } = Array.Empty<NamedType>();

    IPoweredScalarParsingData IPoweredScalarDefinition.ParsingData => ParsingData;

    protected APoweredScalarDefinition(TParsingData parsingData) : base(parsingData) { }

    public virtual bool Equals(APoweredScalarDefinition<TParsingData, TLocations> other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Quantity == other.Quantity && SecondaryQuantities.SequenceEqual(other.SecondaryQuantities);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Quantity.GetHashCode() ^ SecondaryQuantities.GetSequenceHashCode();
    }
}
