namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct AliasUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string AliasOf)
    : IUnitInstanceAttributeParameters, IDerivedUnitInstanceAttributeParameters
{
    public static AliasUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<AliasUnitInstanceAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<AliasUnitInstanceAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    string IDerivedUnitInstanceAttributeParameters.DerivedFrom => AliasOf;

    private static AliasUnitInstanceAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        AliasOf: string.Empty
    );

    private static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<AliasUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            AliasOf
        };

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<AliasUnitInstanceAttributeParameters> AliasOf { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.AliasOf),
            setter: static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters
        );
    }
}