namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct IncludeBasesAttributeParameters(IEnumerable<string> IncludedBases)
{
    public static IncludeBasesAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IncludeBasesAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<IncludeBasesAttributeParameters, IncludeBasesAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IncludeBasesAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<IncludeBasesAttributeParameters, IncludeBasesAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static IncludeBasesAttributeParameters Defaults { get; } = new
    (
        IncludedBases: Array.Empty<string>()
    );

    private static Dictionary<string, AttributeProperty<IncludeBasesAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<IncludeBasesAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

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
