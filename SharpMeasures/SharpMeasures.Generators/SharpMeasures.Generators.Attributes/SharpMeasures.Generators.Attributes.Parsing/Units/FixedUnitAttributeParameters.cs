namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct FixedUnitAttributeParameters(string Name, string Plural, double Value, double Bias)
    : IUnitAttributeParameters
{
    public static FixedUnitAttributeParameters Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<FixedUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<FixedUnitAttributeParameters, FixedUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<FixedUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<FixedUnitAttributeParameters, FixedUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    private static FixedUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Value: 0,
        Bias: 0
    );

    private static Dictionary<string, AttributeProperty<FixedUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<FixedUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<FixedUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Value,
            Bias
        };

        private static AttributeProperty<FixedUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(UnitAliasAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<FixedUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(UnitAliasAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<FixedUnitAttributeParameters> Value { get; } = new
        (
            name: nameof(FixedUnitAttribute.Value),
            setter: static (parameters, obj) => obj is double value ? parameters with { Value = value } : parameters
        );

        private static AttributeProperty<FixedUnitAttributeParameters> Bias { get; } = new
        (
            name: nameof(FixedUnitAttribute.Bias),
            setter: static (parameters, obj) => obj is double bias ? parameters with { Bias = bias } : parameters
        );
    }
}