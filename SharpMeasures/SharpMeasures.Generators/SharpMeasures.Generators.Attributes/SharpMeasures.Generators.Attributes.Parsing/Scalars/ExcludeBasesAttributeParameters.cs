namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct ExcludeBasesAttributeParameters(IEnumerable<string> ExcludedBases)
{
    public static ParameterParser<ExcludeBasesAttributeParameters, ExcludeBasesAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static ExcludeBasesAttributeParameters Defaults => new
    (
        ExcludedBases: Array.Empty<string>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<ExcludeBasesAttributeParameters>> AllProperties => new()
        {
            ExcludedBases
        };

        private static AttributeProperty<ExcludeBasesAttributeParameters> ExcludedBases { get; } = new
        (
            name: nameof(ExcludeBasesAttribute.ExcludedBases),
            setter: static (parameters, obj) => obj is IEnumerable<string> excludedUnits
                ? parameters with { ExcludedBases = excludedUnits }
                : parameters
        );
    }
}
