namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class RawDerivableUnitDefinition : ARawAttributeDefinition<DerivableUnitLocations>
{
    internal static RawDerivableUnitDefinition Empty { get; } = new();

    public string? Expression { get; init; }
    public IReadOnlyList<NamedType?> Signature { get; init; } = Array.Empty<NamedType?>();

    private RawDerivableUnitDefinition() : base(DerivableUnitLocations.Empty) { }

    public virtual bool Equals(RawDerivableUnitDefinition other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Expression == other.Expression && Signature.SequenceEqual(other.Signature);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ (Expression?.GetHashCode() ?? 0) ^ Signature.GetSequenceHashCode();
    }
}
