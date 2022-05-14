namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct CacheableDerivableUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Expression,
    MinimalLocation Signature, IReadOnlyList<MinimalLocation> SignatureComponents)
{
    internal static CacheableDerivableUnitLocations Construct(DerivableUnitLocations originalLocations)
    {
        MinimalLocation[] signatureComponents = new MinimalLocation[originalLocations.SignatureComponents.Count];

        for (int i = 0; i < signatureComponents.Length; i++)
        {
            signatureComponents[i] = MinimalLocation.FromLocation(originalLocations.SignatureComponents[i]);
        }

        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Expression), MinimalLocation.FromLocation(originalLocations.Signature), signatureComponents);
    }

    public bool Equals(CacheableDerivableUnitLocations other)
    {
        return Attribute == other.Attribute && AttributeName == other.AttributeName && Expression == other.Expression
            && Signature == other.Signature && SignatureComponents.SequenceEqual(other.SignatureComponents);
    }

    public override int GetHashCode()
    {
        int hashCode = (Attribute, AttributeName, Expression, Signature).GetHashCode();

        foreach (MinimalLocation location in SignatureComponents)
        {
            hashCode ^= location.GetHashCode();
        }

        return hashCode;
    }
}
