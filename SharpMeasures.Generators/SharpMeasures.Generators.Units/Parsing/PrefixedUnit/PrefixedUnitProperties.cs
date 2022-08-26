namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

using System.Collections.Generic;

internal static class PrefixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawPrefixedUnitDefinition>> AllProperties => new IAttributeProperty<RawPrefixedUnitDefinition>[]
    {
        CommonProperties.Name<RawPrefixedUnitDefinition, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Name)),
        CommonProperties.Plural<RawPrefixedUnitDefinition, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawPrefixedUnitDefinition, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.From)),
        MetricPrefix,
        BinaryPrefix
    };

    private static PrefixedUnitProperty<int> MetricPrefix { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.MetricPrefix),
        setter: static (definition, metricPrefix) => definition with { MetricPrefix = (MetricPrefixName)metricPrefix },
        locator: static (locations, metricPrefixLocation) => locations with { MetricPrefix = metricPrefixLocation }
    );

    private static PrefixedUnitProperty<int> BinaryPrefix { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.BinaryPrefix),
        setter: static (definition, binaryPrefix) => definition with { BinaryPrefix = (BinaryPrefixName)binaryPrefix },
        locator: static (locations, binaryPrefixLocation) => locations with { BinaryPrefix = binaryPrefixLocation }
    );
}
