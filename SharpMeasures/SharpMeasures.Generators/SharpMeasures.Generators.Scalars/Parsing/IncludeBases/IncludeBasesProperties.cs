namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeBasesDefinition>> AllProperties => new IAttributeProperty<RawIncludeBasesDefinition>[]
    {
        CommonProperties.Items<string?, RawIncludeBasesDefinition, IncludeBasesLocations>(nameof(IncludeBasesAttribute.IncludedBases))
    };
}
