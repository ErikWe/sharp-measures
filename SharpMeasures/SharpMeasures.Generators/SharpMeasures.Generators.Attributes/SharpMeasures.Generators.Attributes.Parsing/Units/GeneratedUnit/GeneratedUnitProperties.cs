namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units;

using System.Collections.Generic;

internal static class GeneratedUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<GeneratedUnitDefinition>> AllProperties => new IAttributeProperty<GeneratedUnitDefinition>[]
    {
        Quantity,
        AllowBias,
        GenerateDocumentation
    };

    private static GeneratedUnitProperty<INamedTypeSymbol> Quantity { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.Quantity),
        setter: static (definition, quantitySymbol) => definition with { Quantity = quantitySymbol.AsNamedType() },
        locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
    );

    private static GeneratedUnitProperty<bool> AllowBias { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.AllowBias),
        setter: static (definition, allowBias) => definition with { AllowBias = allowBias },
        locator: static (locations, allowBiasLocation) => locations with { AllowBias = allowBiasLocation }
    );

    private static GeneratedUnitProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedUnitAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) =>
        {
            return definition with
            {
                GenerateDocumentation = generateDocumentation,
                ParsingData = definition.ParsingData with { ExplicitlySetGenerateDocumentation = true }
            };
        },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
