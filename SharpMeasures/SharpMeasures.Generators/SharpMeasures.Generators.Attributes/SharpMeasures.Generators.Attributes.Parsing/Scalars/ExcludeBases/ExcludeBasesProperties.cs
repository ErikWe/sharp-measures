namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class ExcludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeBasesDefinition>> AllProperties => new IAttributeProperty<RawExcludeBasesDefinition>[]
    {
        ItemLists.CommonProperties.Items<string?, RawExcludeBasesDefinition, ExcludeBasesLocations>(nameof(ExcludeBasesAttribute.ExcludedBases))
    };
}
