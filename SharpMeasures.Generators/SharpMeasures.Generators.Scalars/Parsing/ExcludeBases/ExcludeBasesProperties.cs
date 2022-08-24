namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

using System.Collections.Generic;

internal static class ExcludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawUnitListDefinition>> AllProperties => new IAttributeProperty<RawUnitListDefinition>[]
    {
        CommonProperties.Items<string?, RawUnitListDefinition, UnitListLocations>(nameof(ExcludeBasesAttribute.ExcludedBases))
    };
}
