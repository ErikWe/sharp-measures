namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSharpMeasuresUnitDefinition>> AllProperties => new IAttributeProperty<RawSharpMeasuresUnitDefinition>[]
    {
        Quantity,
        BiasTerm,
        GenerateDocumentation
    };

    private static SharpMeasuresUnitProperty<INamedTypeSymbol> Quantity { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.Quantity),
        setter: static (definition, quantitySymbol) => definition with { Quantity = quantitySymbol.AsNamedType() },
        locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
    );

    private static SharpMeasuresUnitProperty<bool> BiasTerm { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.BiasTerm),
        setter: static (definition, biasTerm) => definition with { BiasTerm = biasTerm },
        locator: static (locations, biasTermLocation) => locations with { BiasTerm = biasTermLocation }
    );

    private static SharpMeasuresUnitProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresUnitAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
