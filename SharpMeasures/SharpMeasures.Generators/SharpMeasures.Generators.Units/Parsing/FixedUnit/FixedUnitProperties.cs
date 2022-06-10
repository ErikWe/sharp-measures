namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class FixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawFixedUnitDefinition>> AllProperties => new IAttributeProperty<RawFixedUnitDefinition>[]
    {
        CommonProperties.Name<RawFixedUnitDefinition, FixedUnitParsingData, FixedUnitLocations>(nameof(FixedUnitAttribute.Name)),
        CommonProperties.Plural<RawFixedUnitDefinition, FixedUnitParsingData, FixedUnitLocations>(nameof(FixedUnitAttribute.Plural)),
        Value,
        Bias
    };

    private static FixedUnitProperty<double> Value { get; } = new
    (
        name: nameof(FixedUnitAttribute.Value),
        setter: static (definition, value) => definition with { Value = value },
        locator: static (locations, valueLocation) => locations with { Value = valueLocation }
    );

    private static FixedUnitProperty<double> Bias { get; } = new
    (
        name: nameof(FixedUnitAttribute.Bias),
        setter: static (definition, bias) => definition with { Bias = bias },
        locator: static (locations, biasLocation) => locations with { Bias = biasLocation }
    );
}
