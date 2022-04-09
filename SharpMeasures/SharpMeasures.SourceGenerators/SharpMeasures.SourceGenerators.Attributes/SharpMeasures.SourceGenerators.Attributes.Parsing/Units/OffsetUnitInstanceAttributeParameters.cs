namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct OffsetUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string From, double Offset)
    : IUnitInstanceAttributeParameters, IDerivedUnitInstanceAttributeParameters
{
    string IDerivedUnitInstanceAttributeParameters.DerivedFrom => From;

    public static OffsetUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        OffsetUnitInstanceAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static OffsetUnitInstanceAttributeParameters Defaults { get; } = new
        (
            Name: string.Empty,
            Plural: string.Empty,
            Symbol: string.Empty,
            IsSIUnit: false,
            IsConstant: false,
            From: string.Empty,
            Offset: 0
        );

        public static Dictionary<string, AttributeProperty<OffsetUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<OffsetUnitInstanceAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<OffsetUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Offset
        };

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> From { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<OffsetUnitInstanceAttributeParameters> Offset { get; } = new
        (
            name: nameof(OffsetUnitInstanceAttribute.Offset),
            setter: static (parameters, obj) => obj is double offset ? parameters with { Offset = offset } : parameters
        );
    }
}