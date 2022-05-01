namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class CubableQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<CubableQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        SecondaryQuantities
    });

    public static AttributeProperty<CubableQuantityParameters> Quantity { get; } = new
    (
        name: nameof(CubableQuantityAttribute.Quantity),
        setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
    );

    public static AttributeProperty<CubableQuantityParameters> SecondaryQuantities { get; } = new
    (
        name: nameof(CubableQuantityAttribute.SecondaryQuantities),
        setter: static (parameters, obj) => obj is INamedTypeSymbol[] secondarySquareRootQuantities
            ? parameters with { SecondaryQuantities = Array.AsReadOnly(secondarySquareRootQuantities) }
            : parameters
    );
}
