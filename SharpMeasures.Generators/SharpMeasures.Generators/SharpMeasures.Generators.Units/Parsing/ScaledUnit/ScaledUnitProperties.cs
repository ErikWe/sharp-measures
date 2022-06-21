﻿namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class ScaledUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawScaledUnitDefinition>> AllProperties => new IAttributeProperty<RawScaledUnitDefinition>[]
    {
        CommonProperties.Name<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Name)),
        CommonProperties.Plural<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.From)),
        Scale,
        Expression
    };

    private static ScaledUnitProperty<double> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (definition, scale) => definition with { Scale = scale },
        locator: static (locations, scaleLocation) => locations with { Scale = scaleLocation }
    );

    private static ScaledUnitProperty<string> Expression { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Expression),
        setter: static (definition, expression) => definition with { Expression = expression },
        locator: static (locations, expressionLocation) => locations with { Expression = expressionLocation }
    );
}
