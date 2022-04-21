namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Scalars;

using System;
using System.Collections.Generic;
using System.Linq;

public readonly record struct InvertibleQuantityAttributeParameters(INamedTypeSymbol? Quantity, IEnumerable<INamedTypeSymbol> SecondaryQuantities)
{
    public static InvertibleQuantityAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static InvertibleQuantityAttributeParameters Parse(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingle<InvertibleQuantityAttributeParameters, InvertibleQuantityAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static InvertibleQuantityAttributeParameters Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingle(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseSiIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseSingleIndices<InvertibleQuantityAttributeParameters, InvertibleQuantityAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseSingleIndices(attributeData, ConstructorParameters, NamedParameters);

    private static InvertibleQuantityAttributeParameters Defaults { get; } = new
    (
        Quantity: null,
        SecondaryQuantities: Array.Empty<INamedTypeSymbol>()
    );

    private static Dictionary<string, AttributeProperty<InvertibleQuantityAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<InvertibleQuantityAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<InvertibleQuantityAttributeParameters>> AllProperties => new()
        {
            Quantity,
            SecondaryQuantities
        };

        private static AttributeProperty<InvertibleQuantityAttributeParameters> Quantity { get; } = new
        (
            name: nameof(InvertibleQuantityAttribute.Quantity),
            setter: static (parameters, obj) => obj is INamedTypeSymbol squareRootQuantity ? parameters with { Quantity = squareRootQuantity } : parameters
        );

        private static AttributeProperty<InvertibleQuantityAttributeParameters> SecondaryQuantities { get; } = new
        (
            name: nameof(InvertibleQuantityAttribute.SecondaryQuantities),
            setter: static (parameters, obj) => obj is IEnumerable<INamedTypeSymbol> secondarySquareRootQuantities
                ? parameters with { SecondaryQuantities = secondarySquareRootQuantities }
                : parameters
        );
    }
}
