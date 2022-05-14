namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class DerivedUnitProperties
{
    public static IReadOnlyList<AttributeProperty<DerivedUnitDefinition>> AllProperties => new[]
    {
        Name,
        Plural,
        Signature,
        Units
    };

    public static AttributeProperty<DerivedUnitDefinition> Name { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Name),
        setter: static (definition, obj) => obj is string name ? definition with { Name = name } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateName(syntax, index) }
    );

    public static AttributeProperty<DerivedUnitDefinition> Plural { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Plural),
        setter: static (definition, obj) => obj is string plural ? definition with { Plural = plural } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocatePlural(syntax, index) }
    );

    public static AttributeProperty<DerivedUnitDefinition> Signature { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Signature),
        setter: static (definition, obj) => obj is INamedTypeSymbol[] signature ? definition.ParseSignature(signature) : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateSignature(syntax, index) }
    );

    public static AttributeProperty<DerivedUnitDefinition> Units { get; } = new
    (
        name: nameof(DerivedUnitAttribute.Units),
        setter: static (definition, obj) => obj is string[] units ? definition with { Units = units } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateUnits(syntax, index) }
    );
}
