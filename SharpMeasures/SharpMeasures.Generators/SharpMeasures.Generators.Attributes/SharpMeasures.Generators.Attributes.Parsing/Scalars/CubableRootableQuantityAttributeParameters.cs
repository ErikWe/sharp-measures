namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct CubableQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static ParameterParser<CubableQuantityAttributeParameters, CubableQuantityAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static CubableQuantityAttributeParameters Defaults => new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<CubableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<CubableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(CubableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<CubableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(CubableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
