namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class ScaledUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<ScaledUnitDefinition>> AllProperties => new IAttributeProperty<ScaledUnitDefinition>[]
    {
        CommonProperties.Name<ScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Name)),
        CommonProperties.Plural<ScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Plural)),
        CommonProperties.DependantOn<ScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.From)),
        Scale
    };

    private static ScaledUnitProperty<double> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (definition, scale) => definition with { Scale = scale },
        locator: static (locations, scaleLocation) => locations with { Scale = scaleLocation }
    );
}
