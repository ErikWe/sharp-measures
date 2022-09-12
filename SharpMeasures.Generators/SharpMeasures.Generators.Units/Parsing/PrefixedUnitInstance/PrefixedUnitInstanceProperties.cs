namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class PrefixedUnitInstanceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawPrefixedUnitInstanceDefinition>> AllProperties => new IAttributeProperty<RawPrefixedUnitInstanceDefinition>[]
    {
        CommonProperties.Name<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>(nameof(PrefixedUnitInstanceAttribute.Name)),
        CommonProperties.PluralForm<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>(nameof(PrefixedUnitInstanceAttribute.PluralForm)),
        CommonProperties.PluralFormRegexSubstitution<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>(nameof(PrefixedUnitInstanceAttribute.PluralFormRegexSubstitution)),
        CommonProperties.OriginalUnitInstance<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>(nameof(PrefixedUnitInstanceAttribute.OriginalUnitInstance)),
        MetricPrefix,
        BinaryPrefix
    };

    private static PrefixedUnitInstanceProperty<int> MetricPrefix { get; } = new
    (
        name: nameof(PrefixedUnitInstanceAttribute.MetricPrefix),
        setter: static (definition, metricPrefix) => definition with { MetricPrefix = (MetricPrefixName)metricPrefix },
        locator: static (locations, metricPrefixLocation) => locations with { MetricPrefix = metricPrefixLocation }
    );

    private static PrefixedUnitInstanceProperty<int> BinaryPrefix { get; } = new
    (
        name: nameof(PrefixedUnitInstanceAttribute.BinaryPrefix),
        setter: static (definition, binaryPrefix) => definition with { BinaryPrefix = (BinaryPrefixName)binaryPrefix },
        locator: static (locations, binaryPrefixLocation) => locations with { BinaryPrefix = binaryPrefixLocation }
    );
}
