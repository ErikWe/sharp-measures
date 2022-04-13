namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct SquarableQuantityAttributeParameters(Type? Quantity, IEnumerable<Type> SecondaryQuantities)
{
    public static SquarableQuantityAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static SquarableQuantityAttributeParameters? Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static SquarableQuantityAttributeParameters? Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static SquarableQuantityAttributeParameters Defaults { get; } = new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<Type>()
    );

    private static Dictionary<string, AttributeProperty<SquarableQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<SquarableQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<SquarableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<SquarableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(SquarableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is Type squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<SquarableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(SquarableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<Type> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
