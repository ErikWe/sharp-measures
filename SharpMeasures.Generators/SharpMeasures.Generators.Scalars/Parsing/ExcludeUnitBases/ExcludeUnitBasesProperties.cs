namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class ExcludeUnitBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeUnitBasesDefinition>> AllProperties => new IAttributeProperty<RawExcludeUnitBasesDefinition>[]
    {
        CommonProperties.Items<string?, RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations>(nameof(ExcludeUnitBasesAttribute.ExcludedUnitBases))
    };
}
