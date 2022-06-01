namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class IncludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeUnitsDefinition>> AllProperties => new IAttributeProperty<RawIncludeUnitsDefinition>[]
    {
        ItemLists.CommonProperties.Items<string?, RawIncludeUnitsDefinition, IncludeUnitsLocations>(nameof(IncludeUnitsAttribute.IncludedUnits))
    };
}
