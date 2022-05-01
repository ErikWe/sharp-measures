namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.ObjectModel;

internal static class GeneratedScalarQuantityProperties
{
    public static ReadOnlyCollection<AttributeProperty<GeneratedScalarQuantityParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Unit,
        Biased
    });

    public static AttributeProperty<GeneratedScalarQuantityParameters> Unit { get; } = new
    (
        name: nameof(GeneratedScalarQuantityAttribute.Unit),
        setter: static (parameters, obj) => parameters with { Unit = obj as INamedTypeSymbol }
    );

    public static AttributeProperty<GeneratedScalarQuantityParameters> Biased { get; } = new
    (
        name: nameof(GeneratedScalarQuantityAttribute.Biased),
        setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
    );
}
