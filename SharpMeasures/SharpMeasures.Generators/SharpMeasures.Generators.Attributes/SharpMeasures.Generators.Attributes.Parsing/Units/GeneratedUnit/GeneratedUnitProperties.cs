namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System;
using System.Collections.ObjectModel;

internal static class GeneratedUnitProperties
{
    public static ReadOnlyCollection<AttributeProperty<GeneratedUnitDefinition>> AllProperties => Array.AsReadOnly(new[]
    {
        Quantity,
        AllowBias,
        GenerateDocumentation
    });

    public static AttributeProperty<GeneratedUnitDefinition> Quantity { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Quantity),
        setter: static (definition, obj) => definition with { Quantity = obj as INamedTypeSymbol },
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateQuantity(syntax, index) }
    );

    public static AttributeProperty<GeneratedUnitDefinition> AllowBias { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.AllowBias),
        setter: static (definition, obj) => obj is bool allowBias ? definition with { AllowBias = allowBias } : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateAllowBias(syntax, index) }
    );

    public static AttributeProperty<GeneratedUnitDefinition> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.GenerateDocumentation),
        setter: static (definition, obj) => obj is bool generateDocumentation
            ? definition with { GenerateDocumentation = generateDocumentation, ParsingData = definition.ParsingData with { ExplicitGenerateDocumentation = true } }
            : definition,
        syntaxSetter: static (definition, syntax, index) => definition with { Locations = definition.Locations.LocateGenerateDocumentation(syntax, index) }
    );
}
