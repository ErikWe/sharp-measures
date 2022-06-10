namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class ExcludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeUnitsDefinition>> AllProperties => new IAttributeProperty<RawExcludeUnitsDefinition>[]
    {
        CommonProperties.Items<string?, RawExcludeUnitsDefinition, ExcludeUnitsLocations>(nameof(ExcludeUnitsAttribute.ExcludedUnits))
    };
}
