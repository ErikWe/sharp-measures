namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct IncludeUnitsAttributeParameters(IEnumerable<string> IncludedUnits)
{
    public static ParameterParser<IncludeUnitsAttributeParameters, IncludeUnitsAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static IncludeUnitsAttributeParameters Defaults => new
    (
        IncludedUnits: Array.Empty<string>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<IncludeUnitsAttributeParameters>> AllProperties => new()
        {
            IncludedUnits
        };

        private static AttributeProperty<IncludeUnitsAttributeParameters> IncludedUnits { get; } = new
        (
            name: nameof(IncludeUnitsAttribute.IncludedUnits),
            setter: static (parameters, obj) => obj is IEnumerable<string> includedUnits
                ? parameters with { IncludedUnits = includedUnits }
                : parameters
        );
    }
}
