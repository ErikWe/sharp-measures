namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class IncludeUnitBasesProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeUnitBasesDefinition>> AllProperties => new IAttributeProperty<RawIncludeUnitBasesDefinition>[]
    {
        CommonProperties.Items<string?, RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations>(nameof(IncludeUnitBasesAttribute.IncludedUnitBases)),
        StackingMode
    };

    private static IncludeUnitBasesProperty<InclusionStackingMode> StackingMode { get; } = new
    (
        name: nameof(IncludeUnitBasesAttribute.StackingMode),
        setter: static (definition, stackingMode) => definition with { StackingMode = stackingMode },
        locator: static (locations, stackingModeLocation) => locations with { StackingMode = stackingModeLocation }
    );
}
