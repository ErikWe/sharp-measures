namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct CacheableDerivableUnitDefinition(string Expression, IReadOnlyList<NamedType> Signature, IReadOnlyList<NamedType> Quantities,
    CacheableDerivableUnitLocations Locations, DerivableUnitParsingData ParsingData)
{
    internal static CacheableDerivableUnitDefinition Construct(DerivableUnitDefinition originalDefinition)
    {
        NamedType[] signature = new NamedType[originalDefinition.Signature.Count];
        NamedType[] quantities = new NamedType[originalDefinition.Quantities.Count];

        for (int i = 0; i < signature.Length; i++)
        {
            signature[i] = NamedType.FromSymbol(originalDefinition.Signature[i]);
        }

        for (int i = 0; i < quantities.Length; i++)
        {
            quantities[i] = NamedType.FromSymbol(originalDefinition.Quantities[i]);
        }

        return new(originalDefinition.Expression, signature, quantities, originalDefinition.Locations.ToCacheable(), originalDefinition.ParsingData);
    }

    public bool Equals(CacheableDerivableUnitDefinition other)
    {
        return Expression == other.Expression && Locations == other.Locations && Signature.SequenceEqual(other.Signature)
            && Quantities.SequenceEqual(other.Quantities) && ParsingData == other.ParsingData;
    }

    public override int GetHashCode()
    {
        int hashCode = (Expression, Locations).GetHashCode();

        foreach (NamedType signature in Signature)
        {
            hashCode ^= signature.GetHashCode();
        }

        foreach (NamedType quantity in Quantities)
        {
            hashCode ^= quantity.GetHashCode();
        }

        return hashCode;
    }
}
