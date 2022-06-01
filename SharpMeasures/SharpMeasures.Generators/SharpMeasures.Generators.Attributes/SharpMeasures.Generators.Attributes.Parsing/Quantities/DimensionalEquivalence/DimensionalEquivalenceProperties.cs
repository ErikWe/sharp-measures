﻿namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

internal static class DimensionalEquivalenceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDimensionalEquivalenceDefinition>> AllProperties => new IAttributeProperty<RawDimensionalEquivalenceDefinition>[]
    {
        ItemLists.CommonProperties.Items<INamedTypeSymbol, NamedType?, RawDimensionalEquivalenceDefinition, DimensionalEquivalenceLocations>
            (nameof(DimensionalEquivalenceAttribute.Quantities), static (x) => x.AsNamedType()),
        CastOperatorBehaviour
    };

    private static DimensionalEquivalenceProperty<int> CastOperatorBehaviour { get; } = new
    (
        name: nameof(DimensionalEquivalenceAttribute.CastOperatorBehaviour),
        setter: static (definition, castOperatorBehaviour) => definition with { CastOperatorBehaviour = (ConversionOperationBehaviour)castOperatorBehaviour },
        locator: static (locations, castOperatorBehaviourLocation) => locations with { CastOperatorBehaviour = castOperatorBehaviourLocation }
    );
}
