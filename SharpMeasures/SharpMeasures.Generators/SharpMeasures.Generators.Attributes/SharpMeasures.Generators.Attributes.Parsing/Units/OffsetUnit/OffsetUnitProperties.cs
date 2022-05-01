namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class OffsetUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<OffsetUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        From,
        Offset
    });

    public static AttributeProperty<OffsetUnitParameters> Name { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<OffsetUnitParameters> Plural { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<OffsetUnitParameters> From { get; } = new
    (
        name: nameof(OffsetUnitAttribute.From),
        setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
    );

    public static AttributeProperty<OffsetUnitParameters> Offset { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Offset),
        setter: static (parameters, obj) => obj is double offset ? parameters with { Offset = offset } : parameters
    );
}
