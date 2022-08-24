namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

using System.Collections.Generic;

internal static class IncludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitListDefinition>> AllProperties => new IAttributeProperty<RawUnitListDefinition>[]
    {
        CommonProperties.Items<string?, RawUnitListDefinition, UnitListLocations>(nameof(IncludeUnitsAttribute.IncludedUnits))
    };
}
