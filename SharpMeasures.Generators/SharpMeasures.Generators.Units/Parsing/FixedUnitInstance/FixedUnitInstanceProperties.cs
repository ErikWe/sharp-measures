namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal static class FixedUnitInstanceProperties
{
    public static IReadOnlyList<IAttributeProperty<RawFixedUnitInstanceDefinition>> AllProperties => new IAttributeProperty<RawFixedUnitInstanceDefinition>[]
    {
        CommonProperties.Name<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>(nameof(FixedUnitInstanceAttribute.Name)),
        CommonProperties.PluralForm<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>(nameof(FixedUnitInstanceAttribute.PluralForm))
    };
}
