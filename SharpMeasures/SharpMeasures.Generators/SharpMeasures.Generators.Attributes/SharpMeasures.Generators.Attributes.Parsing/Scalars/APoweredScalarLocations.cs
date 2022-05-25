namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public abstract record class APoweredScalarLocations : AAttributeLocations, IPoweredScalarLocations
{
    public MinimalLocation Quantity { get; init; }

    public MinimalLocation SecondaryQuantitiesCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SecondaryQuantitiesElements { get; init; } = Array.Empty<MinimalLocation>();

    public virtual bool Equals(APoweredScalarLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Quantity == other.Quantity && SecondaryQuantitiesCollection == other.SecondaryQuantitiesCollection
            && SecondaryQuantitiesElements.SequenceEqual(other.SecondaryQuantitiesElements);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ (Quantity, SecondaryQuantitiesCollection).GetHashCode() ^ SecondaryQuantitiesElements.GetSequenceHashCode();
    }
}
