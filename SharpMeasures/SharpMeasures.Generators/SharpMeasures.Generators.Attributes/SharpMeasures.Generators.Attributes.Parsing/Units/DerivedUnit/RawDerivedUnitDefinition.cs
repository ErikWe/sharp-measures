namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class RawDerivedUnitDefinition : ARawUnitDefinition<DerivedUnitParsingData, DerivedUnitLocations>
{
    public static RawDerivedUnitDefinition Empty { get; } = new();

    public IReadOnlyList<NamedType?> Signature { get; init; } = Array.Empty<NamedType?>();
    public IReadOnlyList<string?> Units { get; init; } = Array.Empty<string?>();

    private RawDerivedUnitDefinition() : base(DerivedUnitLocations.Empty, DerivedUnitParsingData.Empty) { }

    public virtual bool Equals(RawDerivedUnitDefinition other)
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