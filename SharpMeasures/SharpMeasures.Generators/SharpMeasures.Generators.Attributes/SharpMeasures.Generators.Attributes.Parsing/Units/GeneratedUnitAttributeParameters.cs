namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedUnitAttributeParameters(INamedTypeSymbol? Quantity, bool Biased)
{
    public static GeneratedUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedUnitAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<GeneratedUnitAttributeParameters, GeneratedUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedUnitAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<GeneratedUnitAttributeParameters, GeneratedUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static GeneratedUnitAttributeParameters Defaults => new
    (
        Quantity: null,
        Biased: false
    );

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedUnitAttributeParameters>> AllProperties => new()
        {
            Quantity,
            Biased
        };

        private static AttributeProperty<GeneratedUnitAttributeParameters> Quantity { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Quantity),
            setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedUnitAttributeParameters> Biased { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Biased),
            setter: static (parameters, obj) => obj is bool biased ? parameters with { Biased = biased } : parameters
        );
    }
}