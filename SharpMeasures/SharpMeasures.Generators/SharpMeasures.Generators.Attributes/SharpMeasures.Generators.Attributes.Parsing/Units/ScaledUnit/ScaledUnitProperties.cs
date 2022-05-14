namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class ScaledUnitProperties
{
    public static IReadOnlyList<AttributeProperty<ScaledUnitDefinition>> AllProperties => new[]
    {
        Name,
        Plural,
        From,
        Scale
    };

    public static AttributeProperty<ScaledUnitDefinition> Name { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Name),
        setter: static (definition, obj) => obj is string name ? definition with { Name = name } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<ScaledUnitDefinition> Plural { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Plural),
        setter: static (definition, obj) => obj is string plural ? definition with { Plural = plural } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<ScaledUnitDefinition> From { get; } = new
    (
        name: nameof(ScaledUnitAttribute.From),
        setter: static (definition, obj) => obj is string from ? definition with { From = from } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateFrom(syntax, index) }
    );

    public static AttributeProperty<ScaledUnitDefinition> Scale { get; } = new
    (
        name: nameof(ScaledUnitAttribute.Scale),
        setter: static (definition, obj) => obj is double scale ? definition with { Scale = scale } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateScale(syntax, index) }
    );
}
