namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class DerivableUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<UnprocessedDerivableUnitDefinition>> AllProperties => new IAttributeProperty<UnprocessedDerivableUnitDefinition>[]
    {
        DerivationID,
        Expression,
        Signature
    };

    private static DerivableUnitProperty<string> DerivationID { get; } = new
    (
        name: nameof(DerivableUnitAttribute.DerivationID),
        setter: static (definition, derivationID) => definition with { DerivationID = derivationID },
        locator: static (locations, derivationIDLocation) => locations with { DerivationID = derivationIDLocation }
    );

    private static DerivableUnitProperty<string> Expression { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static(locations, expressionLocation) => locations with { Expression = expressionLocation }
    );

    private static DerivableUnitProperty<INamedTypeSymbol[]> Signature { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Signature),
        setter: static (definition, signature) => definition with { Signature = signature.AsNamedTypes() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            SignatureCollection = collectionLocation,
            SignatureElements = elementLocations
        }
    );
}
