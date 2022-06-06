namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeBases>> AllProperties => new IAttributeProperty<RawIncludeBases>[]
    {
        ItemLists.CommonProperties.Items<string?, RawIncludeBases, IncludeBasesLocations>(nameof(IncludeBasesAttribute.IncludedBases))
    };
}
