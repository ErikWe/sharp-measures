namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;

using System.Collections.Generic;
using System.Linq;

public readonly record struct GeneratedBiasedUnitAttributeParameters(INamedTypeSymbol? BiasedQuantity, INamedTypeSymbol? UnbiasedQuantity)
{
    public static GeneratedBiasedUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedBiasedUnitAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static GeneratedBiasedUnitAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static GeneratedBiasedUnitAttributeParameters Defaults { get; } = new
    (
        BiasedQuantity: null,
        UnbiasedQuantity: null
    );

    private static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<GeneratedBiasedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<GeneratedBiasedUnitAttributeParameters>> AllProperties => new()
        {
            BiasedQuantity,
            UnbiasedQuantity
        };

        private static AttributeProperty<GeneratedBiasedUnitAttributeParameters> BiasedQuantity { get; } = new
        (
            name: nameof(GeneratedBiasedUnitAttribute.BiasedQuantity),
            setter: static (parameters, obj) => parameters with { BiasedQuantity = obj as INamedTypeSymbol }
        );

        private static AttributeProperty<GeneratedBiasedUnitAttributeParameters> UnbiasedQuantity { get; } = new
        (
            name: nameof(GeneratedBiasedUnitAttribute.UnbiasedQuantity),
            setter: static (parameters, obj) => parameters with { UnbiasedQuantity = obj as INamedTypeSymbol }
        );
    }
}