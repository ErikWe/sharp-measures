namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class ExcludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<ExcludeBasesDefinition>> AllProperties => new IAttributeProperty<ExcludeBasesDefinition>[]
    {
        ItemLists.CommonProperties.ItemNames<ExcludeBasesDefinition, ExcludeBasesParsingData, ExcludeBasesLocations>(nameof(ExcludeBasesAttribute.ExcludedBases))
    };
}
