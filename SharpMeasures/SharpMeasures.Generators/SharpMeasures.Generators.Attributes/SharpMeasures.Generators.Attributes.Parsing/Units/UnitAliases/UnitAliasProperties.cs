namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class UnitAliasProperties
{
    public static ReadOnlyCollection<AttributeProperty<UnitAliasParameters>> AllProperties => Array.AsReadOnly(new[]
    {
        Name,
        Plural,
        AliasOf
    });

    public static AttributeProperty<UnitAliasParameters> Name { get; } = new
    (
        name: nameof(UnitAliasAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters
    );

    public static AttributeProperty<UnitAliasParameters> Plural { get; } = new
    (
        name: nameof(UnitAliasAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters
    );

    public static AttributeProperty<UnitAliasParameters> AliasOf { get; } = new
    (
        name: nameof(UnitAliasAttribute.AliasOf),
        setter: static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters
    );
}
