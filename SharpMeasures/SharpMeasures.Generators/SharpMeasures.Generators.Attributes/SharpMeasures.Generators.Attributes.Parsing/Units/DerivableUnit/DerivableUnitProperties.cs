namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class DerivableUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<DerivableUnitDefinition>> AllProperties => new IAttributeProperty<DerivableUnitDefinition>[]
    {
        Expression,
        Signature
    };

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
