namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class ExcludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<ExcludeUnitsDefinition>> AllProperties => new IAttributeProperty<ExcludeUnitsDefinition>[]
    {
        ItemLists.CommonProperties.ItemNames<ExcludeUnitsDefinition, ExcludeUnitsParsingData, ExcludeUnitsLocations>(nameof(ExcludeUnitsAttribute.ExcludedUnits))
    };
}
