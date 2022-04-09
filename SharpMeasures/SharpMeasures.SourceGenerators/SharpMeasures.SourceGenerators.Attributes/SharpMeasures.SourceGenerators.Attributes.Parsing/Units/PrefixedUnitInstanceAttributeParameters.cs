namespace SharpMeasures.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using System.Collections.Generic;
using System.Linq;

public readonly record struct PrefixedUnitInstanceAttributeParameters(string Name, string Plural, string Symbol, bool IsSIUnit, bool IsConstant,
    string From, string Prefix)
    : IUnitInstanceAttributeParameters, IDerivedUnitInstanceAttributeParameters
{
    string IDerivedUnitInstanceAttributeParameters.DerivedFrom => From;

    public static PrefixedUnitInstanceAttributeParameters? Parse(AttributeData attributeData)
    {
        PrefixedUnitInstanceAttributeParameters values = Properties.Defaults;

        (bool success, values) = AttributeDataArgumentParser.Parse(attributeData, values, Properties.ConstructorParameters, Properties.NamedParameters);

        return success ? values : null;
    }

    private static class Properties
    {
        public static PrefixedUnitInstanceAttributeParameters Defaults { get; } = new
        (
            Name: string.Empty,
            Plural: string.Empty,
            Symbol: string.Empty,
            IsSIUnit: false,
            IsConstant: false,
            From: string.Empty,
            Prefix: string.Empty
        );

        public static Dictionary<string, AttributeProperty<PrefixedUnitInstanceAttributeParameters>> ConstructorParameters { get; }
        = AllProperties.ToDictionary(static (x) => x.ParameterName);

        public static Dictionary<string, AttributeProperty<PrefixedUnitInstanceAttributeParameters>> NamedParameters { get; }
            = AllProperties.ToDictionary(static (x) => x.Name);

        private static List<AttributeProperty<PrefixedUnitInstanceAttributeParameters>> AllProperties => new()
        {
            Name,
            Plural,
            Symbol,
            IsSIUnit,
            IsConstant,
            From,
            Prefix
        };

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Name { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.Name),
            setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Plural { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.Plural),
            setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Symbol { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.Symbol),
            setter: static (parameters, obj) => obj is string symbol ? parameters with { Symbol = symbol } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> IsSIUnit { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.IsSIUnit),
            setter: static (parameters, obj) => obj is bool isSIUnit ? parameters with { IsSIUnit = isSIUnit } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> IsConstant { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.IsConstant),
            setter: static (parameters, obj) => obj is bool isConstant ? parameters with { IsConstant = isConstant } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> From { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.From),
            setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
        );

        private static AttributeProperty<PrefixedUnitInstanceAttributeParameters> Prefix { get; } = new
        (
            name: nameof(PrefixedUnitInstanceAttribute.Prefix),
            setter: static (parameters, obj) => obj is string prefix ? parameters with { Prefix = prefix } : parameters
        );
    }
}