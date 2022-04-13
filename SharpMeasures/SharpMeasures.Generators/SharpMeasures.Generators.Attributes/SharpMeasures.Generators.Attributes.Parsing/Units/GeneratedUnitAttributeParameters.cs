namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedUnitAttributeParameters(INamedTypeSymbol? Quantity)
{
    public static GeneratedUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedUnitAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedUnitAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static GeneratedUnitAttributeParameters Defaults => new
    (
        Quantity: null
    );

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedUnitAttributeParameters>> AllProperties => new()
        {
            Quantity
        };

        private static AttributeProperty<GeneratedUnitAttributeParameters> Quantity { get; } = new
        (
            name: nameof(GeneratedUnitAttribute.Quantity),
            setter: static (parameters, obj) => parameters with { Quantity = obj as INamedTypeSymbol }
        );
    }
}