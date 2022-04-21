namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct SquareRootableQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static SquareRootableQuantityAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static SquareRootableQuantityAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<SquareRootableQuantityAttributeParameters, SquareRootableQuantityAttribute>(symbol, Defaults,
            ConstructorParameters, NamedParameters);

    public static SquareRootableQuantityAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<SquareRootableQuantityAttributeParameters, SquareRootableQuantityAttribute>(symbol,
            ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static SquareRootableQuantityAttributeParameters Defaults { get; } = new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static Dictionary<string, AttributeProperty<SquareRootableQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<SquareRootableQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<SquareRootableQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<SquareRootableQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(SquareRootableQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<SquareRootableQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(SquareRootableQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
