namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct AliasUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string AliasOf)
    : IUnitInstanceAttributeParameters, IDerivedUnitInstanceAttributeParameters
{
    string IDerivedUnitInstanceAttributeParameters.DerivedFrom => AliasOf;

    public static AliasUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        AliasUnitInstanceAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static AliasUnitInstanceAttributeParameters Defaults { get; } = new
        (
            Name: string.Empty,
            Plural: string.Empty,
            Symbol: string.Empty,
            IsSIUnit: false,
            IsConstant: false,
            AliasOf: string.Empty
        );

        public static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<AliasUnitInstanceAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<AliasUnitInstanceAttributeParameters>> AllProperties => new()
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