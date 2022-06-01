namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class GeneratedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<RawGeneratedUnitDefinition>> AllProperties => new IAttributeProperty<RawGeneratedUnitDefinition>[]
    {
        Quantity,
        SupportsBiasedQuantities,
        GenerateDocumentation
    };

    private static GeneratedUnitProperty<INamedTypeSymbol> Quantity { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Quantity),
        setter: static (definition, quantitySymbol) => definition with { Quantity = quantitySymbol.AsNamedType() },
        locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
    );

    private static GeneratedUnitProperty<bool> SupportsBiasedQuantities { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.SupportsBiasedQuantities),
        setter: static (definition, allowBias) => definition with { SupportsBiasedQuantities = allowBias },
        locator: static (locations, allowBiasLocation) => locations with { SupportsBiasedQuantities = allowBiasLocation }
    );

    private static GeneratedUnitProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
