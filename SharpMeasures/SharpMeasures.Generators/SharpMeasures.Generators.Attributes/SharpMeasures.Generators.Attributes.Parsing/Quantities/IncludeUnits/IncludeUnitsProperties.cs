namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class IncludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<IncludeUnitsDefinition>> AllProperties => new IAttributeProperty<IncludeUnitsDefinition>[]
    {
        ItemLists.CommonProperties.ItemNames<IncludeUnitsDefinition, IncludeUnitsParsingData, IncludeUnitsLocations>(nameof(IncludeUnitsAttribute.IncludedUnits))
    };
}
