namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class ScaledUnitInstanceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawScaledUnitInstanceDefinition>> AllProperties => new IAttributeProperty<RawScaledUnitInstanceDefinition>[]
    {
        CommonProperties.Name<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>(nameof(ScaledUnitInstanceAttribute.Name)),
        CommonProperties.PluralForm<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>(nameof(ScaledUnitInstanceAttribute.PluralForm)),
        CommonProperties.PluralFormRegexSubstitution<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>(nameof(ScaledUnitInstanceAttribute.PluralFormRegexSubstitution)),
        CommonProperties.OriginalUnitInstance<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>(nameof(ScaledUnitInstanceAttribute.OriginalUnitInstance)),
        Scale,
        Expression
    };

    private static ScaledUnitInstanceProperty<double> Scale { get; } = new
    (
        name: nameof(ScaledUnitInstanceAttribute.Scale),
        setter: static (definition, scale) => definition with { Scale = scale },
        locator: static (locations, scaleLocation) => locations with { Scale = scaleLocation }
    );

    private static ScaledUnitInstanceProperty<string> Expression { get; } = new
    (
        name: nameof(ScaledUnitInstanceAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );
}
