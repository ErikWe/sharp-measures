namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class BiasedUnitInstanceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawBiasedUnitInstanceDefinition>> AllProperties => new IAttributeProperty<RawBiasedUnitInstanceDefinition>[]
    {
        CommonProperties.Name<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>(nameof(BiasedUnitInstanceAttribute.Name)),
        CommonProperties.PluralForm<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>(nameof(BiasedUnitInstanceAttribute.PluralForm)),
        CommonProperties.OriginalUnitInstance<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>(nameof(BiasedUnitInstanceAttribute.OriginalUnitInstance)),
        Bias,
        Expression
    };

    private static BiasedUnitInstanceProperty<double> Bias { get; } = new
    (
        name: nameof(BiasedUnitInstanceAttribute.Bias),
        setter: static (definition, bias) => definition with { Bias = bias },
        locator: static (locations, biasLocation) => locations with { Bias = biasLocation }
    );

    private static BiasedUnitInstanceProperty<string> Expression { get; } = new
    (
        name: nameof(BiasedUnitInstanceAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );
}
