namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class DimensionalEquivalenceProperties
{
    public static IReadOnlyList<IAttributeProperty<DimensionalEquivalenceDefinition>> AllProperties => new IAttributeProperty<DimensionalEquivalenceDefinition>[]
    {
        ItemLists.CommonProperties.ItemNames<DimensionalEquivalenceDefinition, DimensionalEquivalenceParsingData,
            DimensionalEquivalenceLocations>(nameof(DimensionalEquivalenceAttribute.Quantities))
    };
}
