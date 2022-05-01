namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class SquareRootableQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<SquareRootableQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        SecondaryQuantities
    });

    public static AttributeProperty<SquareRootableQuantityParameters> Quantity { get; } = new
    (
        name: nameof(SquareRootableQuantityAttribute.Quantity),
        setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
    );

    public static AttributeProperty<SquareRootableQuantityParameters> SecondaryQuantities { get; } = new
    (
        name: nameof(SquareRootableQuantityAttribute.SecondaryQuantities),
        setter: static (parameters, obj) => obj is INamedTypeSymbol[] secondarySquareRootQuantities
            ? parameters with { SecondaryQuantities = Array.AsReadOnly(secondarySquareRootQuantities) }
            : parameters
    );
}
