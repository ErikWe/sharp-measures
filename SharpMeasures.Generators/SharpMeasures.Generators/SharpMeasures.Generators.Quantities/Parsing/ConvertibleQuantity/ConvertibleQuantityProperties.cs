namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal static class ConvertibleQuantityProperties
{
    public static IReadOnlyList<IAttributeProperty<UnprocessedConvertibleQuantityDefinition>> AllProperties => new IAttributeProperty<UnprocessedConvertibleQuantityDefinition>[]
    {
        CommonProperties.Items<INamedTypeSymbol, NamedType?, UnprocessedConvertibleQuantityDefinition, ConvertibleQuantityLocations>
            (nameof(ConvertibleQuantityAttribute.Quantities), static (x) => x?.AsNamedType()),
        Bidirectional,
        CastOperatorBehaviour
    };

    private static ConvertibleQuantityProperty<bool> Bidirectional { get; } = new
    (
        name: nameof(ConvertibleQuantityAttribute.Bidirectional),
        setter: static (definition, bidirectional) => definition with { Bidirectional = bidirectional },
        locator: static (locations, bidirectionalLocation) => locations with { Bidirectional = bidirectionalLocation }
    );

    private static ConvertibleQuantityProperty<int> CastOperatorBehaviour { get; } = new
    (
        name: nameof(ConvertibleQuantityAttribute.CastOperatorBehaviour),
        setter: static (definition, castOperatorBehaviour) => definition with { CastOperatorBehaviour = (ConversionOperatorBehaviour)castOperatorBehaviour },
        locator: static (locations, castOperatorBehaviourLocation) => locations with { CastOperatorBehaviour = castOperatorBehaviourLocation }
    );
}
