namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

internal static class IncludeUnitsProperties
{
    public static IReadOnlyList<IAttributeProperty<RawIncludeUnitsDefinition>> AllProperties => new IAttributeProperty<RawIncludeUnitsDefinition>[]
    {
        CommonProperties.Items<string?, RawIncludeUnitsDefinition, IncludeUnitsLocations>(nameof(IncludeUnitsAttribute.IncludedUnits)),
        StackingMode
    };

    private static IncludeUnitsProperty<InclusionStackingMode> StackingMode { get; } = new
    (
        name: nameof(IncludeUnitsAttribute.StackingMode),
        setter: static (definition, stackingMode) => definition with { StackingMode = stackingMode },
        locator: static (locations, stackingModeLocation) => locations with { StackingMode = stackingModeLocation }
    );
}
