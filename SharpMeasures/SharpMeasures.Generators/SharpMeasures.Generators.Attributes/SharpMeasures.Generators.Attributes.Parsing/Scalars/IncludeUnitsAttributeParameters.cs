namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct IncludeUnitsAttributeParameters(IEnumerable<string> IncludedUnits)
{
    public static IncludeUnitsAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IncludeUnitsAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IncludeUnitsAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static IncludeUnitsAttributeParameters Defaults { get; } = new
    (
        IncludedUnits: Array.Empty<string>()
    );

    private static Dictionary<string, AttributeProperty<IncludeUnitsAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<IncludeUnitsAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

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
