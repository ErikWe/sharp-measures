namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class GeneratedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawGeneratedUnitDefinition>> AllProperties => new IAttributeProperty<RawGeneratedUnitDefinition>[]
    {
        Quantity,
        BiasTerm,
        GenerateDocumentation
    };

    private static GeneratedUnitProperty<INamedTypeSymbol> Quantity { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.Quantity),
        setter: static (definition, quantitySymbol) => definition with { Quantity = quantitySymbol.AsNamedType() },
        locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
    );

    private static GeneratedUnitProperty<bool> BiasTerm { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.BiasTerm),
        setter: static (definition, biasTerm) => definition with { BiasTerm = biasTerm },
        locator: static (locations, biasTermLocation) => locations with { BiasTerm = biasTermLocation }
    );

    private static GeneratedUnitProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
