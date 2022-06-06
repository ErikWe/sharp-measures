namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class PrefixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawPrefixedUnit>> AllProperties => new IAttributeProperty<RawPrefixedUnit>[]
    {
        CommonProperties.Name<RawPrefixedUnit, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Name)),
        CommonProperties.Plural<RawPrefixedUnit, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawPrefixedUnit, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.From)),
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
