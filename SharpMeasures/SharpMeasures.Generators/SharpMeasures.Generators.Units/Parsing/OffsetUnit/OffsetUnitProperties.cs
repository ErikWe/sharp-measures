namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class OffsetUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawOffsetUnitDefinition>> AllProperties => new IAttributeProperty<RawOffsetUnitDefinition>[]
    {
        CommonProperties.Name<RawOffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations>(nameof(BiasedUnitAttribute.Name)),
        CommonProperties.Plural<RawOffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations>(nameof(BiasedUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawOffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations>(nameof(BiasedUnitAttribute.From)),
        Offset
    };

    private static OffsetUnitProperty<double> Offset { get; } = new
    (
        name: nameof(BiasedUnitAttribute.Bias),
        setter: static (definition, offset) => definition with { Offset = offset },
        locator: static (locations, offsetLocation) => locations with { Offset = offsetLocation }
    );
}
