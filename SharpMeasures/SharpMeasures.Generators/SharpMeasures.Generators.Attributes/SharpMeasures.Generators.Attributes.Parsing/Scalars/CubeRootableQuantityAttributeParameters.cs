namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct CubeRootableQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static ParameterParser<CubeRootableQuantityAttributeParameters, CubeRootableQuantityAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static CubeRootableQuantityAttributeParameters Defaults => new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<CubeRootableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<CubeRootableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(CubeRootableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<CubeRootableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(CubeRootableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
