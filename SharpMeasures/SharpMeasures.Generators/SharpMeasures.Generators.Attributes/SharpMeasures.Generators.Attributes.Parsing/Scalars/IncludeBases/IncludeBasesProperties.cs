namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeBasesDefinition>> AllProperties => new IAttributeProperty<RawIncludeBasesDefinition>[]
    {
        ItemLists.CommonProperties.Items<string?, RawIncludeBasesDefinition, IncludeBasesLocations>(nameof(IncludeBasesAttribute.IncludedBases))
    };
}
