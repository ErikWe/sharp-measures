namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct SquareRootableQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static ParameterParser<SquareRootableQuantityAttributeParameters, SquareRootableQuantityAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static SquareRootableQuantityAttributeParameters Defaults => new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<SquareRootableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<SquareRootableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(SquareRootableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<SquareRootableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(SquareRootableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
