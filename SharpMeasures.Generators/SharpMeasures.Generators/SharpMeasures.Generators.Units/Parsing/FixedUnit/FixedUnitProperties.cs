namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class FixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawFixedUnitDefinition>> AllProperties => new IAttributeProperty<RawFixedUnitDefinition>[]
    {
        CommonProperties.Name<RawFixedUnitDefinition, FixedUnitLocations>(nameof(FixedUnitAttribute.Name)),
        CommonProperties.Plural<RawFixedUnitDefinition, FixedUnitLocations>(nameof(FixedUnitAttribute.Plural))
    };
}
