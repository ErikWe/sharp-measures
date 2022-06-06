namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class OffsetUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawOffsetUnit>> AllProperties => new IAttributeProperty<RawOffsetUnit>[]
    {
        CommonProperties.Name<RawOffsetUnit, OffsetUnitParsingData, OffsetUnitLocations>(nameof(OffsetUnitAttribute.Name)),
        CommonProperties.Plural<RawOffsetUnit, OffsetUnitParsingData, OffsetUnitLocations>(nameof(OffsetUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawOffsetUnit, OffsetUnitParsingData, OffsetUnitLocations>(nameof(OffsetUnitAttribute.From)),
        Offset
    };

    private static OffsetUnitProperty<double> Offset { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Offset),
        setter: static (definition, offset) => definition with { Offset = offset },
        locator: static (locations, offsetLocation) => locations with { Offset = offsetLocation }
    );
}
