namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct FixedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    double Value, double Bias)
    : IUnitInstanceAttributeParameters
{
    public static FixedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        FixedUnitInstanceAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static FixedUnitInstanceAttributeParameters Defaults { get; } = new
        (
            Name: string.Empty,
            Plural: string.Empty,
            Symbol: string.Empty,
            IsSIUnit: false,
            IsConstant: false,
            Value: 0,
            Bias: 0
        );

        public static Dictionary<string, AttributeProperty<FixedUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<FixedUnitInstanceAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<FixedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            Value,
            Bias
        };

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(AliasUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> Value { get; } = new
        (
            name: nameof(FixedUnitInstanceAttribute.Value),
            setter: static (parameters, obj) => obj is double value ? parameters with { Value = value } : parameters
        );

        private static AttributeProperty<FixedUnitInstanceAttributeParameters> Bias { get; } = new
        (
            name: nameof(FixedUnitInstanceAttribute.Bias),
            setter: static (parameters, obj) => obj is double bias ? parameters with { Bias = bias } : parameters
        );
    }
}