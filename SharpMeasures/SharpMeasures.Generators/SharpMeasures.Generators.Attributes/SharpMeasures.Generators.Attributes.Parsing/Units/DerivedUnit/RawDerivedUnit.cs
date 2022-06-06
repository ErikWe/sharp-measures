namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class RawDerivedUnit : ARawUnitDefinition<DerivedUnitParsingData, DerivedUnitLocations>
{
    public static RawDerivedUnit Empty { get; } = new();

    public IReadOnlyList<NamedType?> Signature { get; init; } = Array.Empty<NamedType?>();
    public IReadOnlyList<string?> Units { get; init; } = Array.Empty<string?>();

    private RawDerivedUnit() : base(DerivedUnitLocations.Empty, DerivedUnitParsingData.Empty) { }

    public virtual bool Equals(RawDerivedUnit other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Signature.SequenceEqual(other.Signature) && Units.SequenceEqual(other.Units);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Signature.GetSequenceHashCode() ^ Units.GetSequenceHashCode();
    }
}
