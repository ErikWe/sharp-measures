﻿namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;
using System.Linq;

public readonly record struct UnitAliasAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string AliasOf)
    : IUnitAttributeParameters, IDerivedUnitAttributeParameters
{
    public static UnitAliasAttributeParameters? Parse(AttributeData attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<UnitAliasAttributeParameters> Parse(INamedTypeSymbol symbol)
        => ParameterParser.Parse<UnitAliasAttributeParameters, UnitAliasAttribute>(symbol, Defaults, ConstructorParameters, NamedParameters);

    public static IEnumerable<UnitAliasAttributeParameters> Parse(IEnumerable<AttributeData> attributeData)
        => ParameterParser.Parse(attributeData, Defaults, ConstructorParameters, NamedParameters);

    public static IDictionary<string, int> ParseIndices(AttributeData attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(INamedTypeSymbol symbol)
        => ParameterParser.ParseIndices<UnitAliasAttributeParameters, UnitAliasAttribute>(symbol, ConstructorParameters, NamedParameters);

    public static IEnumerable<IDictionary<string, int>> ParseIndices(IEnumerable<AttributeData> attributeData)
        => ParameterParser.ParseIndices(attributeData, ConstructorParameters, NamedParameters);

    string IDerivedUnitAttributeParameters.DerivedFrom => AliasOf;

    private static UnitAliasAttributeParameters Defaults { get; } = new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Symbol: string.Empty,
        IsSIUnit: false,
        IsConstant: false,
        AliasOf: string.Empty
    );

    private static Dictionary<string, AttributeProperty<UnitAliasAttributeParameters>> ConstructorParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.ParameterName);

    private static Dictionary<string, AttributeProperty<UnitAliasAttributeParameters>> NamedParameters { get; }
        = Properties.AllProperties.ToDictionary(static (x) => x.Name);

    private static class Properties
    {
        public static List<AttributeProperty<UnitAliasAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            AliasOf
        };

        private static AttributeProperty<UnitAliasAttributeParameters> Name { get; } = new
        (
            name: nameof(UnitAliasAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> Plural { get; } = new
        (
            name: nameof(UnitAliasAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> Symbol { get; } = new
        (
            name: nameof(UnitAliasAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(UnitAliasAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(UnitAliasAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<UnitAliasAttributeParameters> AliasOf { get; } = new
        (
            name: nameof(UnitAliasAttribute.AliasOf),
            setter: static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters
        );
    }
}