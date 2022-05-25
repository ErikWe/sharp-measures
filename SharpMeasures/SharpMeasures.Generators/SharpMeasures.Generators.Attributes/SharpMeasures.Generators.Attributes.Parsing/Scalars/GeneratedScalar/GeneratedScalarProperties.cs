namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class GeneratedScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<GeneratedScalarDefinition>> AllProperties => new IAttributeProperty<GeneratedScalarDefinition>[]
    {
        Unit,
        Vector,
        Biased,
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static GeneratedScalarProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Vector),
        setter: static (definition, vector) => definition with
        { 
            Vector = vector.AsNamedType(),
            ParsingData = definition.ParsingData with { SpecifiedVector = true }
        },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static GeneratedScalarProperty<bool> Biased { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Biased),
        setter: static (definition, biased) => definition with { Biased = biased },
        locator: static (locations, biasedLocation) => locations with { Biased = biasedLocation }
    );

    private static GeneratedScalarProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static GeneratedScalarProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static GeneratedScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with
        {
            GenerateDocumentation = generateDocumentation,
            ParsingData = definition.ParsingData with { ExplicitlyDisabledDocumentation = generateDocumentation is false }
        },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
