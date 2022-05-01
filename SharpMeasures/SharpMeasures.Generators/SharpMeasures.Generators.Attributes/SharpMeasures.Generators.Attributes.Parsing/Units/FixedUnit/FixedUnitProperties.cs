namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class FixedUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<FixedUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        Value,
        Bias
    });

    public static AttributeProperty<FixedUnitParameters> Name { get; } = new
    (
        name: nameof(UnitAliasAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<FixedUnitParameters> Plural { get; } = new
    (
        name: nameof(UnitAliasAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<FixedUnitParameters> Value { get; } = new
    (
        name: nameof(FixedUnitAttribute.Value),
        setter: static (parameters, obj) => obj is double value ? parameters with { Value = value } : parameters
    );

    public static AttributeProperty<FixedUnitParameters> Bias { get; } = new
    (
        name: nameof(FixedUnitAttribute.Bias),
        setter: static (parameters, obj) => obj is double bias ? parameters with { Bias = bias } : parameters
    );
}
