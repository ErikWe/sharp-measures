namespace SharpMeasures.Generators.Quantities.Parsing.DimensionalEquivalence;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

internal static class DimensionalEquivalenceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawDimensionalEquivalenceDefinition>> AllProperties => new IAttributeProperty<RawDimensionalEquivalenceDefinition>[]
    {
        CommonProperties.Items<INamedTypeSymbol, NamedType?, RawDimensionalEquivalenceDefinition, DimensionalEquivalenceLocations>
            (nameof(DimensionalEquivalenceAttribute.Quantities), static (x) => x?.AsNamedType()),
        CastOperatorBehaviour
    };

    private static DimensionalEquivalenceProperty<int> CastOperatorBehaviour { get; } = new
    (
        name: nameof(DimensionalEquivalenceAttribute.CastOperatorBehaviour),
        setter: static (definition, castOperatorBehaviour) => definition with { CastOperatorBehaviour = (ConversionOperationBehaviour)castOperatorBehaviour },
        locator: static (locations, castOperatorBehaviourLocation) => locations with { CastOperatorBehaviour = castOperatorBehaviourLocation }
    );
}
