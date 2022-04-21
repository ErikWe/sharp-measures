namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct ExcludeUnitsAttributeParameters(IEnumerable<string> ExcludedUnits)
{
    public static ExcludeUnitsAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static ExcludeUnitsAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<ExcludeUnitsAttributeParameters, ExcludeUnitsAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static ExcludeUnitsAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<ExcludeUnitsAttributeParameters, ExcludeUnitsAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static ExcludeUnitsAttributeParameters Defaults { get; } = new
    (
        ExcludedUnits: Array.Empty<string>()
    );

    private static Dictionary<string, AttributeProperty<ExcludeUnitsAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ExcludeUnitsAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

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
