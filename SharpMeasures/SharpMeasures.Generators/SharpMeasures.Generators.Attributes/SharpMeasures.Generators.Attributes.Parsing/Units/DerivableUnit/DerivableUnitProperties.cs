namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class DerivableUnitProperties
{
    public static IReadOnlyList<AttributeProperty<DerivableUnitDefinition>> AllProperties => new[]
    {
        Expression,
        Signature
    };

    public static AttributeProperty<DerivableUnitDefinition> Expression { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Expression),
        setter: static (definition, obj) => obj is string expression ? definition with { Expression = expression } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateExpression(syntax, index) }
    );

    public static AttributeProperty<DerivableUnitDefinition> Signature { get; } = new
    (
        name: nameof(DerivableUnitAttribute.Signature),
        setter: static (definition, obj) => obj is INamedTypeSymbol[] signature ? definition.ParseSignature(signature) : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateSignature(syntax, index) }
    );
}
