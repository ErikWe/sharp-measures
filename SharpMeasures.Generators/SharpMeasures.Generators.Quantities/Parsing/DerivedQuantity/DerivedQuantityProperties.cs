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
        OperatorImplementation,
        Permutations
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

    private static DerivedQuantityProperty<int> OperatorImplementation { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.OperatorImplementation),
        setter: static (definition, operatorImplementation) => definition with { OperatorImplementation = (DerivationOperatorImplementation)operatorImplementation },
        locator: static (locations, operatorImplementationLocation) => locations with { OperatorImplementation = operatorImplementationLocation }
    );

    private static DerivedQuantityProperty<bool> Permutations { get; } = new
    (
        name: nameof(DerivedQuantityAttribute.Permutations),
        setter: static (definition, permutations) => definition with { Permutations = permutations },
        locator: static (locations, permutationsLocation) => locations with { Permutations = permutationsLocation }
    );
}
