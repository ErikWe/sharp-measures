namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class PrefixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<PrefixedUnitDefinition>> AllProperties => new IAttributeProperty<PrefixedUnitDefinition>[]
    {
        CommonProperties.Name<PrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Name)),
        CommonProperties.Plural<PrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.Plural)),
        CommonProperties.DependantOn<PrefixedUnitDefinition, PrefixedUnitParsingData, PrefixedUnitLocations>(nameof(PrefixedUnitAttribute.From)),
        MetricPrefixName,
        BinaryPrefixName
    };

    private static PrefixedUnitProperty<int> MetricPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.MetricPrefixName),
        setter: static (definition, metricPrefixName) =>
        {
            var modifiedParsingData = definition.ParsingData with { SpecifiedPrefixType = PrefixedUnitParsingData.PrefixType.Metric };

            return definition with
            {
                MetricPrefixName = (MetricPrefixName)metricPrefixName,
                ParsingData = modifiedParsingData
            };
        },
        locator: static (locations, metricPrefixNameLocation) => locations with { MetricPrefixName = metricPrefixNameLocation }
    );

    private static PrefixedUnitProperty<int> BinaryPrefixName { get; } = new
    (
        name: nameof(PrefixedUnitAttribute.BinaryPrefixName),
        setter: static (definition, binaryPrefixName) =>
        {
            var modifiedParsingData = definition.ParsingData with { SpecifiedPrefixType = PrefixedUnitParsingData.PrefixType.Binary };

            return definition with
            {
                BinaryPrefixName = (BinaryPrefixName)binaryPrefixName,
                ParsingData = modifiedParsingData
            };
        },
        locator: static (locations, binaryPrefixNameLocation) => locations with { BinaryPrefixName = binaryPrefixNameLocation }
    );
}
