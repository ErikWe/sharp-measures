namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class ScaledUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<ScaledUnitParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        From,
        Scale
    });

    public static AttributeProperty<ScaledUnitParameters> Name { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<ScaledUnitParameters> Plural { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<ScaledUnitParameters> From { get; } = new
    (
        name: nameof(ScaledUnitAttribute.From),
        setter: static (parameters, obj) => obj is string from ? parameters with { From = from } : parameters
    );

    public static AttributeProperty<ScaledUnitParameters> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (parameters, obj) => obj is double scale ? parameters with { Scale = scale } : parameters
    );
}
