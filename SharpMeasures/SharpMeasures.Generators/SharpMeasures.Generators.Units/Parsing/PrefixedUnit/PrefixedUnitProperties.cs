namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class PrefixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawPrefixedUnitDefinition>> AllProperties => new IAttributeProperty<RawPrefixedUnitDefinition>[]
    {
        CommonProperties.Name<RawPrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Name)),
        CommonProperties.Plural<RawPrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawPrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.From)),
        MetricPrefixName,
        BinaryPrefixName
    };

    private static PrefixedUnitProperty<int> MetricPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.MetricPrefixName),
        setter: static (definition, metricPrefixName) => definition with { MetricPrefixName = (MetricPrefixName)metricPrefixName },
        locator: static (locations, metricPrefixNameLocation) => locations with { MetricPrefixName = metricPrefixNameLocation }
    );

    private static PrefixedUnitProperty<int> BinaryPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.BinaryPrefixName),
        setter: static (definition, binaryPrefixName) => definition with { BinaryPrefixName = (BinaryPrefixName)binaryPrefixName },
        locator: static (locations, binaryPrefixNameLocation) => locations with { BinaryPrefixName = binaryPrefixNameLocation }
    );
}
