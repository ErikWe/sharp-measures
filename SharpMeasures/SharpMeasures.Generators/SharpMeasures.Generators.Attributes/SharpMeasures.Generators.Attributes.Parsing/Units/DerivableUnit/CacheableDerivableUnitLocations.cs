namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using System.Collections.Generic;

public readonly record struct CacheableDerivableUnitLocations(MinimalLocation Attribute, MinimalLocation AttributeName, MinimalLocation Expression,
    MinimalLocation Signature, IReadOnlyList<MinimalLocation> SignatureComponents)
{
    internal static CacheableDerivableUnitLocations Construct(DerivableUnitLocations originalLocations)
    {
        MinimalLocation[] signatureComponents = new MinimalLocation[originalLocations.SignatureComponent.Count];

        for (int i = 0; i < signatureComponents.Length; i++)
        {
            signatureComponents[i] = MinimalLocation.FromLocation(originalLocations.SignatureComponent[i]);
        }

        return new(MinimalLocation.FromLocation(originalLocations.Attribute), MinimalLocation.FromLocation(originalLocations.AttributeName),
            MinimalLocation.FromLocation(originalLocations.Expression), MinimalLocation.FromLocation(originalLocations.Signature), signatureComponents);
    }
}
