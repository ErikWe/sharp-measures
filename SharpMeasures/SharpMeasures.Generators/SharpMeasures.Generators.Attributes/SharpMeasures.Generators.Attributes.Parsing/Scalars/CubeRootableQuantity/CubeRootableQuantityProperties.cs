namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class CubeRootableQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<CubeRootableQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        SecondaryQuantities
    });

    public static AttributeProperty<CubeRootableQuantityParameters> Quantity { get; } = new
    (
        name: nameof(CubeRootableQuantityAttribute.Quantity),
        setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
    );

    public static AttributeProperty<CubeRootableQuantityParameters> SecondaryQuantities { get; } = new
    (
        name: nameof(CubeRootableQuantityAttribute.SecondaryQuantities),
        setter: static (parameters, obj) => obj is INamedTypeSymbol[] secondarySquareRootQuantities
            ? parameters with { SecondaryQuantities = Array.AsReadOnly(secondarySquareRootQuantities) }
            : parameters
    );
}
