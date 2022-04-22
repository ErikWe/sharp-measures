namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Attributes.Parsing.Utility;
using SharpMeasures.Generators.Units;

using System.Collections.Generic;

public readonly record struct FixedUnitAttributeParameters(string Name, string Plural, double Value, double Bias)
    : IUnitAttributeParameters
{
    public static ParameterParser<FixedUnitAttributeParameters, FixedUnitAttribute> Parser { get; }
        = new(Properties.AllProperties, Defaults);

    private static FixedUnitAttributeParameters Defaults => new
    (
        Name: string.Empty,
        Plural: string.Empty,
        Value: 0,
        Bias: 0
    );

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