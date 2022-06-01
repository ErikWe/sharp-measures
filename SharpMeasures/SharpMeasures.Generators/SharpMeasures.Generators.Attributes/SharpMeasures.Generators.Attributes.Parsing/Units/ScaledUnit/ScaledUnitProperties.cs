namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class ScaledUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawScaledUnitDefinition>> AllProperties => new IAttributeProperty<RawScaledUnitDefinition>[]
    {
        CommonProperties.Name<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Name)),
        CommonProperties.Plural<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.Plural)),
        CommonProperties.DependantOn<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations>(nameof(ScaledUnitAttribute.From)),
        Scale
    };

    private static ScaledUnitProperty<double> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (definition, scale) => definition with { Scale = scale },
        locator: static (locations, scaleLocation) => locations with { Scale = scaleLocation }
    );
}
