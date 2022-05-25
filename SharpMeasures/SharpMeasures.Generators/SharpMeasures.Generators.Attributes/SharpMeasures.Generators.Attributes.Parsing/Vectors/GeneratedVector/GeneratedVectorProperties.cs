namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal static class GeneratedVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<GeneratedVectorDefinition>> AllProperties => new IAttributeProperty<GeneratedVectorDefinition>[]
    {
        Unit,
        Scalar,
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static GeneratedVectorProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static GeneratedVectorProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.Scalar),
        setter: static (definition, scalar) => definition with
        {
            Scalar = scalar.AsNamedType(),
            ParsingData = definition.ParsingData with { SpecifiedScalar = true }
        },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static GeneratedVectorProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static GeneratedVectorProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static GeneratedVectorProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with
        {
            GenerateDocumentation = generateDocumentation,
            ParsingData = definition.ParsingData with { ExplicitlyDisabledDocumentation = generateDocumentation is false }
        },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
