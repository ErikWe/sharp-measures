namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class SquarableQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<SquarableQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        SecondaryQuantities
    });

    public static AttributeProperty<SquarableQuantityParameters> Quantity { get; } = new
    (
        name: nameof(SquarableQuantityAttribute.Quantity),
        setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
    );

    public static AttributeProperty<SquarableQuantityParameters> SecondaryQuantities { get; } = new
    (
        name: nameof(SquarableQuantityAttribute.SecondaryQuantities),
        setter: static (parameters, obj) => obj is INamedTypeSymbol[] secondarySquareRootQuantities
            ? parameters with { SecondaryQuantities = Array.AsReadOnly(secondarySquareRootQuantities) }
            : parameters
    );
}
