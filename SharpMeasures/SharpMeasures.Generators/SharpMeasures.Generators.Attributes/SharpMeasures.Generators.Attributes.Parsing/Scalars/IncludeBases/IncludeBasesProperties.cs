namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<IncludeBasesDefinition>> AllProperties => new IAttributeProperty<IncludeBasesDefinition>[]
    {
        ItemLists.CommonProperties.ItemNames<IncludeBasesDefinition, IncludeBasesParsingData, IncludeBasesLocations>(nameof(IncludeBasesAttribute.IncludedBases))
    };
}
