namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class OffsetUnitProperties
{
    public static IReadOnlyList<AttributeProperty<OffsetUnitDefinition>> AllProperties => new[]
    {
        Name,
        Plural,
        From,
        Offset
    };

    public static AttributeProperty<OffsetUnitDefinition> Name { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Name),
        setter: static (definition, obj) => obj is string name ? definition with { Name = name } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<OffsetUnitDefinition> Plural { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Plural),
        setter: static (definition, obj) => obj is string plural ? definition with { Plural = plural } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<OffsetUnitDefinition> From { get; } = new
    (
        name: nameof(OffsetUnitAttribute.From),
        setter: static (definition, obj) => obj is string from ? definition with { From = from } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateFrom(syntax, index) }
    );

    public static AttributeProperty<OffsetUnitDefinition> Offset { get; } = new
    (
        name: nameof(OffsetUnitAttribute.Offset),
        setter: static (definition, obj) => obj is double offset ? definition with { Offset = offset } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateOffset(syntax, index) }
    );
}
