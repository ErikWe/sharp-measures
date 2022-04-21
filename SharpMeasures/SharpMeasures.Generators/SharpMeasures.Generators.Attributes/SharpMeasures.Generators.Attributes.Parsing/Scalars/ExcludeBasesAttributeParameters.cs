namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct ExcludeBasesAttributeParameters(IEnumerable<string> ExcludedBases)
{
    public static ExcludeBasesAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static ExcludeBasesAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<ExcludeBasesAttributeParameters, ExcludeBasesAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static ExcludeBasesAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<ExcludeBasesAttributeParameters, ExcludeBasesAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static ExcludeBasesAttributeParameters Defaults { get; } = new
    (
        ExcludedBases: Array.Empty<string>()
    );

    private static Dictionary<string, AttributeProperty<ExcludeBasesAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<ExcludeBasesAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

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
