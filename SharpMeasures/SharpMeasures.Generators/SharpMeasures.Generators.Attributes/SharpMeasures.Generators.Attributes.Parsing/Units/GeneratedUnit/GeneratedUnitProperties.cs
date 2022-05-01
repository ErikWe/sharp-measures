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
        AllowBias
    });

    public static AttributeProperty<GeneratedUnitParameters> Quantity { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Quantity),
        setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
    );

    public static AttributeProperty<GeneratedUnitParameters> AllowBias { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.AllowBias),
        setter: static (parameters, obj) => obj is bool allowBias ? parameters with { AllowBias = allowBias } : parameters
    );
}
