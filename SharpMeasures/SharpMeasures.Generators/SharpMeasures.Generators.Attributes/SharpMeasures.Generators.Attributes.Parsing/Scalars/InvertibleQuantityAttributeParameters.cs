namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct InvertibleQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static ParameterParser<InvertibleQuantityAttributeParameters, InvertibleQuantityAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static InvertibleQuantityAttributeParameters Defaults => new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<InvertibleQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<InvertibleQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(InvertibleQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<InvertibleQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(InvertibleQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
