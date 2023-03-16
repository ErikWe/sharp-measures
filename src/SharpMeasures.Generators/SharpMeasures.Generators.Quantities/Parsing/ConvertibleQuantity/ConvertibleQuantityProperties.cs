namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class ConvertibleQuantityProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicConvertibleQuantityDefinition>> AllProperties => new IAttributeProperty<SymbolicConvertibleQuantityDefinition>[]
    {
        CommonProperties.Items<INamedTypeSymbol?, INamedTypeSymbol?, SymbolicConvertibleQuantityDefinition, ConvertibleQuantityLocations>(nameof(ConvertibleQuantityAttribute.Quantities), static (x) => x),
        ConversionDirection,
        CastOperatorBehaviour
    };

    private static ConvertibleQuantityProperty<int> ConversionDirection { get; } = new
    (
        name: nameof(ConvertibleQuantityAttribute.ConversionDirection),
        setter: static (definition, conversionDirection) => definition with { ConversionDirection = (QuantityConversionDirection)conversionDirection },
        locator: static (locations, conversionDirectionLocation) => locations with { ConversionDirection = conversionDirectionLocation }
    );

    private static ConvertibleQuantityProperty<int> CastOperatorBehaviour { get; } = new
    (
        name: nameof(ConvertibleQuantityAttribute.CastOperatorBehaviour),
        setter: static (definition, castOperatorBehaviour) => definition with { CastOperatorBehaviour = (ConversionOperatorBehaviour)castOperatorBehaviour },
        locator: static (locations, castOperatorBehaviourLocation) => locations with { CastOperatorBehaviour = castOperatorBehaviourLocation }
    );
}
