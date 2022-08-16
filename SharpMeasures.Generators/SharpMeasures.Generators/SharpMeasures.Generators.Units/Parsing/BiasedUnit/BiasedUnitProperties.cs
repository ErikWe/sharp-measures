namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class BiasedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<UnprocessedBiasedUnitDefinition>> AllProperties => new IAttributeProperty<UnprocessedBiasedUnitDefinition>[]
    {
        CommonProperties.Name<UnprocessedBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.Name)),
        CommonProperties.Plural<UnprocessedBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.Plural)),
        CommonProperties.DependantOn<UnprocessedBiasedUnitDefinition, BiasedUnitLocations>(nameof(BiasedUnitAttribute.From)),
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
