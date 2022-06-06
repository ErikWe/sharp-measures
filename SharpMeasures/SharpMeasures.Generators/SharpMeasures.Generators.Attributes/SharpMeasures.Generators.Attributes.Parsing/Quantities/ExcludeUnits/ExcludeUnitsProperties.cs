namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class ExcludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeUnits>> AllProperties => new IAttributeProperty<RawExcludeUnits>[]
    {
        ItemLists.CommonProperties.Items<string?, RawExcludeUnits, ExcludeUnitsLocations>(nameof(ExcludeUnitsAttribute.ExcludedUnits))
    };
}
