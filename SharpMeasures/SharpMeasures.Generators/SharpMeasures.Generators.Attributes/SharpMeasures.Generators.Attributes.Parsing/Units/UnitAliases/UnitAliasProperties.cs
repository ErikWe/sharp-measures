namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class UnitAliasProperties
{
    public static IReadOnlyList<AttributeProperty<UnitAliasDefinition>> AllProperties => new[]
    {
        Name,
        Plural,
        AliasOf
    };

    public static AttributeProperty<UnitAliasDefinition> Name { get; } = new
    (
        name: nameof(UnitAliasAttribute.Name),
        setter: static (parameters, obj) => obj is string name ? parameters with { Name = name } : parameters,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<UnitAliasDefinition> Plural { get; } = new
    (
        name: nameof(UnitAliasAttribute.Plural),
        setter: static (parameters, obj) => obj is string plural ? parameters with { Plural = plural } : parameters,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<UnitAliasDefinition> AliasOf { get; } = new
    (
        name: nameof(UnitAliasAttribute.AliasOf),
        setter: static (parameters, obj) => obj is string aliasOf ? parameters with { AliasOf = aliasOf } : parameters,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateAliasOf(syntax, index) }
    );
}
