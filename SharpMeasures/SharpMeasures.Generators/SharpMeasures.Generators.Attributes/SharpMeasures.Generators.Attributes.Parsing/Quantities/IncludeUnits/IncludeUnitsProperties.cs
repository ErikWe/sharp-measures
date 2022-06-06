namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class IncludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeUnits>> AllProperties => new IAttributeProperty<RawIncludeUnits>[]
    {
        ItemLists.CommonProperties.Items<string?, RawIncludeUnits, IncludeUnitsLocations>(nameof(IncludeUnitsAttribute.IncludedUnits))
    };
}
