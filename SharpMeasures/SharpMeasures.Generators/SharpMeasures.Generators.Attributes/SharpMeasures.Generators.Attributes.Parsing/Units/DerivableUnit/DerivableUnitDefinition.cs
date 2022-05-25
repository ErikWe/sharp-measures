namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public record class DerivableUnitDefinition : AAttributeDefinition<DerivableUnitParsingData, DerivableUnitLocations>
{
    internal static DerivableUnitDefinition Empty { get; } = new();

    public string Expression { get; init; } = string.Empty;
    public IReadOnlyList<NamedType> Signature { get; init; } = Array.Empty<NamedType>();

    private DerivableUnitDefinition() : base(DerivableUnitParsingData.Empty) { }

    public virtual bool Equals(DerivableUnitDefinition other)
    {
        if (other is null)
        {
            return false;
        }

        return base.Equals(other) && Expression == other.Expression && Signature.SequenceEqual(other.Signature);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode() ^ Expression.GetHashCode() ^ Signature.GetSequenceHashCode();
    }
}
