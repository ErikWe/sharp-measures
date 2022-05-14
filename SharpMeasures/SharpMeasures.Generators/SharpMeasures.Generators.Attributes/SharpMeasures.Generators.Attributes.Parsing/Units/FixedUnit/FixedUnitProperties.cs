namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class FixedUnitProperties
{
    public static IReadOnlyList<AttributeProperty<FixedUnitDefinition>> AllProperties => new[]
    {
        Name,
        Plural,
        Value,
        Bias
    };

    public static AttributeProperty<FixedUnitDefinition> Name { get; } = new
    (
        name: nameof(UnitAliasAttribute.Name),
        setter: static (definition, obj) => obj is string name ? definition with { Name = name } : definition,
        syntaxSetter: static(definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<FixedUnitDefinition> Plural { get; } = new
    (
        name: nameof(UnitAliasAttribute.Plural),
        setter: static (definition, obj) => obj is string plural ? definition with { Plural = plural } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<FixedUnitDefinition> Value { get; } = new
    (
        name: nameof(FixedUnitAttribute.Value),
        setter: static (definition, obj) => obj is double value ? definition with { Value = value } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateValue(syntax, index) }
    );

    public static AttributeProperty<FixedUnitDefinition> Bias { get; } = new
    (
        name: nameof(FixedUnitAttribute.Bias),
        setter: static (definition, obj) => obj is double bias ? definition with { Bias = bias } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateBias(syntax, index) }
    );
}
