namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class BiasedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawBiasedUnitDefinition>> AllProperties => new IAttributeProperty<RawBiasedUnitDefinition>[]
    {
        CommonProperties.Name<RawBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.Name)),
        CommonProperties.Plural<RawBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.From)),
        Bias,
        Expression
    };

    private static BiasedUnitProperty<double> Bias { get; } = new
    (
        name: nameof(BiasedUnitAttribute.Bias),
        setter: static (definition, bias) => definition with { Bias = bias },
        locator: static (locations, biasLocation) => locations with { Bias = biasLocation }
    );

    private static BiasedUnitProperty<string> Expression { get; } = new
    (
        name: nameof(BiasedUnitAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );
}
