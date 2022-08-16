namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class FixedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<UnprocessedFixedUnitDefinition>> AllProperties => new IAttributeProperty<UnprocessedFixedUnitDefinition>[]
    {
        CommonProperties.Name<UnprocessedFixedUnitDefinition, FixedUnitLocations>(nameof(FixedUnitAttribute.Name)),
        CommonProperties.Plural<UnprocessedFixedUnitDefinition, FixedUnitLocations>(nameof(FixedUnitAttribute.Plural))
    };
}
