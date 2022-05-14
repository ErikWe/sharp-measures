namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct CacheableDerivedUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Name,
    MinimalLocation Plural, MinimalLocation Signature, MinimalLocation Units, IReadOnlyList<MinimalLocation> SignatureComponents,
    IReadOnlyList<MinimalLocation> UnitComponents)
{
    internal static CacheableDerivedUnitLocations Construct(DerivedUnitLocations originalLocations)
    {
        MinimalLocation[] signatureComponents = new MinimalLocation[originalLocations.SignatureComponents.Count];
        MinimalLocation[] unitComponents = new MinimalLocation[originalLocations.UnitComponents.Count];

        for (int i = 0; i < signatureComponents.Length; i++)
        {
            signatureComponents[i] = MinimalLocation.FromLocation(originalLocations.SignatureComponents[i]);
        }

        for (int i = 0; i < unitComponents.Length; i++)
        {
            unitComponents[i] = MinimalLocation.FromLocation(originalLocations.UnitComponents[i]);
        }

        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Name), MinimalLocation.FromLocation(originalLocations.Plural),
            MinimalLocation.FromLocation(originalLocations.Signature), MinimalLocation.FromLocation(originalLocations.Units), signatureComponents, unitComponents);
    }

    public bool Equals(CacheableDerivedUnitLocations other)
    {
        return Attribute == other.Attribute && AttributeName == other.AttributeName && Name == other.Name && Plural == other.Plural && Signature == other.Signature &&
            Units == other.Units && SignatureComponents.SequenceEqual(other.SignatureComponents) && UnitComponents.SequenceEqual(other.UnitComponents);
    }

    public override int GetHashCode()
    {
        int hashCode = (Attribute, AttributeName, Name, Plural, Signature, Units).GetHashCode();

        foreach (MinimalLocation location in SignatureComponents)
        {
            hashCode ^= location.GetHashCode();
        }

        foreach (MinimalLocation location in UnitComponents)
        {
            hashCode ^= location.GetHashCode();
        }

        return hashCode;
    }
}