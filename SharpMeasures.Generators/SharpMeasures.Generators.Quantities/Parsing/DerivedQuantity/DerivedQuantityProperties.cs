namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class DerivedQuantityProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDerivedQuantityDefinition>> AllProperties => new IAttributeProperty<RawDerivedQuantityDefinition>[]
    {
        Expression,
        Signature,
        ImplementOperators,
        ImplementAlgebraicallyEquivalentDerivations
    };

    private static DerivedQuantityProperty<string> Expression { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );

    private static DerivedQuantityProperty<INamedTypeSymbol[]> Signature { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.Signature),
        setter: static (definition, signature) => definition with { Signature = signature.AsNamedTypes() },
        locator: static (locations, collectionLocation, elementLocations) => locations with
        {
            SignatureCollection = collectionLocation,
            SignatureElements = elementLocations
        }
    );

    private static DerivedQuantityProperty<bool> ImplementOperators { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.ImplementOperators),
        setter: static (definition, implementOperators) => definition with { ImplementOperators = implementOperators },
        locator: static (locations, implementOperatorsLocation) => locations with { ImplementOperators = implementOperatorsLocation }
    );

    private static DerivedQuantityProperty<bool> ImplementAlgebraicallyEquivalentDerivations { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.ImplementAlgebraicallyEquivalentDerivations),
        setter: static (definition, implementAlgebraicallyEquivalentDerivations) => definition with
        {
            ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivations
        },
        locator: static (locations, implementAlgebraicallyEquivalentDerivationsLocation) => locations with
        {
            ImplementAlgebraicallyEquivalentDerivations = implementAlgebraicallyEquivalentDerivationsLocation
        }
    );
}
