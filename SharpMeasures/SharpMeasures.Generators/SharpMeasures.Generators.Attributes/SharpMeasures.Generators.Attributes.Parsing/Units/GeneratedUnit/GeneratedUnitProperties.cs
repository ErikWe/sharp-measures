namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class GeneratedUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<GeneratedUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        Biased
    });

    public static AttributeProperty<GeneratedUnitParameters> Quantity { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Quantity),
        setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
    );

    public static AttributeProperty<GeneratedUnitParameters> Biased { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Biased),
        setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
    );
}
