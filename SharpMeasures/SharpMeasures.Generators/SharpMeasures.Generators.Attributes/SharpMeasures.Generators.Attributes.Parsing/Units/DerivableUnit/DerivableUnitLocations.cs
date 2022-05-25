namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class DerivableUnitLocations : AAttributeLocations
{
    internal static DerivableUnitLocations Empty { get; } = new();

    public MinimalLocation Expression { get; init; }

    public MinimalLocation SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements { get; init; } = Array.Empty<MinimalLocation>();

    private DerivableUnitLocations() { }

    public virtual bool Equals(DerivableUnitLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Expression == other.Expression && SignatureCollection == other.SignatureCollection
            && SignatureElements.SequenceEqual(other.SignatureElements);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ (Expression, SignatureCollection).GetHashCode() ^ SignatureElements.GetSequenceHashCode();
    }
}
