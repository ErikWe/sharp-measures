namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitListDefinition>> AllProperties => new IAttributeProperty<RawUnitListDefinition>[]
    {
        CommonProperties.Items<string?, RawUnitListDefinition, UnitListLocations>(nameof(IncludeBasesAttribute.IncludedBases))
    };
}
