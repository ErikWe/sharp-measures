namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;

public readonly record struct IncludeBasesAttributeParameters(IEnumerable<string> IncludedBases)
{
    public static ParameterParser<IncludeBasesAttributeParameters, IncludeBasesAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static IncludeBasesAttributeParameters Defaults => new
    (
        IncludedBases: Array.Empty<string>()
    );

    private static class Properties
    {
        public static List<AttributeProperty<IncludeBasesAttributeParameters>> AllProperties => new()
        {
            IncludedBases
        };

        private static AttributeProperty<IncludeBasesAttributeParameters> IncludedBases { get; } = new
        (
            name: nameof(IncludeBasesAttribute.IncludedBases),
            setter: static (parameters, obj) => obj is IEnumerable<string> includedUnits
                ? parameters with { IncludedBases = includedUnits }
                : parameters
        );
    }
}
