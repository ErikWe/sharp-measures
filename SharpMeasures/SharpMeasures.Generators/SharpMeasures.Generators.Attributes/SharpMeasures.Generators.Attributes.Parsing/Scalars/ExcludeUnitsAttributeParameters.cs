namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct ExcludeUnitsAttributeParameters(IEnumerable<string> ExcludedUnits)
{
    public static ParameterParser<ExcludeUnitsAttributeParameters, ExcludeUnitsAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static ExcludeUnitsAttributeParameters Defaults => new
    (
        ExcludedUnits: Array.Empty<string>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<ExcludeUnitsAttributeParameters>> AllProperties => new()
        {
            ExcludedUnits
        };

        private static AttributeProperty<ExcludeUnitsAttributeParameters> ExcludedUnits { get; } = new
        (
            name: nameof(ExcludeUnitsAttribute.ExcludedUnits),
            setter: static (parameters, obj) => obj is IEnumerable<string> excludedUnits
                ? parameters with { ExcludedUnits = excludedUnits }
                : parameters
        );
    }
}
