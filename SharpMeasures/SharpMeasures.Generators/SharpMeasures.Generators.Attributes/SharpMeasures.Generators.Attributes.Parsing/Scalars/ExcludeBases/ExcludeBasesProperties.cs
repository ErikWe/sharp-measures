namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class ExcludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawExcludeBases>> AllProperties => new IAttributeProperty<RawExcludeBases>[]
    {
        ItemLists.CommonProperties.Items<string?, RawExcludeBases, ExcludeBasesLocations>(nameof(ExcludeBasesAttribute.ExcludedBases))
    };
}
