﻿namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class ScaledUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawScaledUnit>> AllProperties => new IAttributeProperty<RawScaledUnit>[]
    {
        CommonProperties.Name<RawScaledUnit, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Name)),
        CommonProperties.Plural<RawScaledUnit, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawScaledUnit, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.From)),
        Scale
    };

    private static ScaledUnitProperty<double> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (definition, scale) => definition with { Scale = scale },
        locator: static (locations, scaleLocation) => locations with { Scale = scaleLocation }
    );
}
