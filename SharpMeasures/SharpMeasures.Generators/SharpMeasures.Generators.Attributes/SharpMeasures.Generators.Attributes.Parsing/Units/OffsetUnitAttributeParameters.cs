namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct OffsetUnitAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string From, double Offset)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static OffsetUnitAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<OffsetUnitAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<OffsetUnitAttributeParameters, OffsetUnitAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<OffsetUnitAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<OffsetUnitAttributeParameters, OffsetUnitAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    string IDerivedUnitAttributeParameters.DerivedFrom => From;

    private static OffsetUnitAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        From: string.Empty,
        Offset: 0
    );

    private static Dictionary<string, AttributeProperty<OffsetUnitAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<OffsetUnitAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<OffsetUnitAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Offset
        };

        private static AttributeProperty<OffsetUnitAttributeParameters> Name { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> Plural { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> Symbol { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(OffsetUnitAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(OffsetUnitAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> From { get; } = new
        (
            name: nameof(OffsetUnitAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<OffsetUnitAttributeParameters> Offset { get; } = new
        (
            name: nameof(OffsetUnitAttribute.Offset),
            setter: static (parameters, obj) => obj is double offset ? parameters with { Offset = offset } : parameters
        );
    }
}