namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal static class ExcludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeUnitsDefinition>> AllProperties => new IAttributeProperty<RawExcludeUnitsDefinition>[]
    {
        ItemLists.CommonProperties.Items<string?, RawExcludeUnitsDefinition, ExcludeUnitsLocations>(nameof(ExcludeUnitsAttribute.ExcludedUnits))
    };
}
