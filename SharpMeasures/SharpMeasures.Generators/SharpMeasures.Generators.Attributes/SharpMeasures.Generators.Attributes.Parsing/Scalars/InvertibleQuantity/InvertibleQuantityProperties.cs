namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class InvertibleQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<InvertibleQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        SecondaryQuantities
    });

    public static AttributeProperty<InvertibleQuantityParameters> Quantity { get; } = new
    (
        name: nameof(InvertibleQuantityAttribute.Quantity),
        setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
    );

    public static AttributeProperty<InvertibleQuantityParameters> SecondaryQuantities { get; } = new
    (
        name: nameof(InvertibleQuantityAttribute.SecondaryQuantities),
        setter: static (parameters, obj) => obj is INamedTypeSymbol[] secondarySquareRootQuantities
            ? parameters with { SecondaryQuantities = Array.AsReadOnly(secondarySquareRootQuantities) }
            : parameters
    );
}
