namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct CacheableDerivedUnitDefinition(string Name, string Plural, IReadOnlyList<NamedType> Signature,
    IReadOnlyList<string> Units, CacheableDerivedUnitLocations Locations, DerivedUnitParsingData ParsingData)
    : IUnitDefinition
{
    internal static CacheableDerivedUnitDefinition Construct(DerivedUnitDefinition originalDefinition)
    {
        NamedType[] signature = new NamedType[originalDefinition.Signature.Count];

        for (int i = 0; i < signature.Length; i++)
        {
            signature[i] = NamedType.FromSymbol(originalDefinition.Signature[i]);
        }

        return new(originalDefinition.Name, originalDefinition.Plural, signature, originalDefinition.Units, originalDefinition.Locations.ToCacheable(),
            originalDefinition.ParsingData);
    }

    public bool Equals(CacheableDerivedUnitDefinition other)
    {
        return Name == other.Name && Plural == other.Plural && Signature.SequenceEqual(other.Signature)
            && Units.SequenceEqual(other.Units) && Locations == other.Locations && ParsingData == other.ParsingData;
    }

    public override int GetHashCode()
    {
        int hashCode = (Name, Plural, Locations, ParsingData).GetHashCode();

        foreach (NamedType signature in Signature)
        {
            hashCode ^= signature.GetHashCode();
        }

        foreach (string unit in Units)
        {
            hashCode ^= unit.GetHashCode();
        }

        return hashCode;
    }
}