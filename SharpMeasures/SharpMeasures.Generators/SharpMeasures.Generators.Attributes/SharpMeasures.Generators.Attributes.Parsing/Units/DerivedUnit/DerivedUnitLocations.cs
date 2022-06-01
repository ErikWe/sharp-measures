namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class DerivedUnitLocations : AUnitLocations
{
    internal static DerivedUnitLocations Empty { get; } = new();

    public MinimalLocation? SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements { get; init; } = Array.Empty<MinimalLocation>();

    public MinimalLocation? UnitsCollection { get; init; }
    public IReadOnlyList<MinimalLocation> UnitsElements { get; init; } = Array.Empty<MinimalLocation>();

    public bool ExplicitlySetSignature => SignatureCollection is not null;
    public bool ExplicitlySetUnits => UnitsCollection is not null;

    private DerivedUnitLocations() { }

    public virtual bool Equals(DerivedUnitLocations other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && SignatureCollection == other.SignatureCollection && UnitsCollection == other.UnitsCollection
            && SignatureElements.SequenceEqual(other.SignatureElements) && UnitsElements.SequenceEqual(other.UnitsElements);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ (SignatureCollection, UnitsCollection).GetHashCode()
            ^ SignatureElements.GetSequenceHashCode() ^ UnitsElements.GetSequenceHashCode();
    }
}