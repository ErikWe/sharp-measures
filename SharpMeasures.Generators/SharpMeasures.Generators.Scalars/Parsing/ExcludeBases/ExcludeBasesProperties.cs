namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class ExcludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeBasesDefinition>> AllProperties => new IAttributeProperty<RawExcludeBasesDefinition>[]
    {
        CommonProperties.Items<string?, RawExcludeBasesDefinition, ExcludeBasesLocations>(nameof(ExcludeBasesAttribute.ExcludedBases))
    };
}
