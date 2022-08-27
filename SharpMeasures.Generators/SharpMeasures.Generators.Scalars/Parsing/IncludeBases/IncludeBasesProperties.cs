namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;
using SharpMeasures.Generators.Utility;

using System.Collections.Generic;

internal static class IncludeBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeBasesDefinition>> AllProperties => new IAttributeProperty<RawIncludeBasesDefinition>[]
    {
        CommonProperties.Items<string?, RawIncludeBasesDefinition, IncludeBasesLocations>(nameof(IncludeBasesAttribute.IncludedBases)),
        StackingMode
    };

    private static IncludeBasesProperty<InclusionStackingMode> StackingMode { get; } = new
    (
        name: nameof(IncludeBasesAttribute.StackingMode),
        setter: static (definition, stackingMode) => definition with { StackingMode = stackingMode },
        locator: static (locations, stackingModeLocation) => locations with { StackingMode = stackingModeLocation }
    );
}
