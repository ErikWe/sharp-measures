namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct CubeRootableQuantityAttributeParameters(Type? Quantity, IEnumerable<Type> SecondaryQuantities)
{
    public static CubeRootableQuantityAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static CubeRootableQuantityAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static CubeRootableQuantityAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static CubeRootableQuantityAttributeParameters Defaults { get; } = new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<Type>()
    );

    private static Dictionary<string, AttributeProperty<CubeRootableQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<CubeRootableQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<CubeRootableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<CubeRootableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(CubeRootableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is Type squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<CubeRootableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(CubeRootableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<Type> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
