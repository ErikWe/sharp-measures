namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class GeneratedScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<RawGeneratedScalarDefinition>> AllProperties => new IAttributeProperty<RawGeneratedScalarDefinition>[]
    {
        Unit,
        Vector,
        Biased,
        DefaultUnitName,
        DefaultUnitSymbol,
        Reciprocal,
        Square,
        Cube,
        SquareRoot,
        CubeRoot,
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
        setter: static (definition, vector) => definition with { Vector = vector.AsNamedType() },
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

    private static GeneratedScalarProperty<INamedTypeSymbol> Reciprocal { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Reciprocal),
        setter: static (definition, reciprocal) => definition with { Reciprocal = reciprocal.AsNamedType() },
        locator: static (locations, reciprocalLocation) => locations with { Reciprocal = reciprocalLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Square { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Square),
        setter: static (definition, square) => definition with { Square = square.AsNamedType() },
        locator: static (locations, squareLocation) => locations with { Square = squareLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Cube { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.Cube),
        setter: static (definition, cube) => definition with { Cube = cube.AsNamedType() },
        locator: static (locations, cubeLocation) => locations with { Cube = cubeLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> SquareRoot { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.SquareRoot),
        setter: static (definition, squareRoot) => definition with { SquareRoot = squareRoot.AsNamedType() },
        locator: static (locations, squareRootLocation) => locations with { SquareRoot = squareRootLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> CubeRoot { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.CubeRoot),
        setter: static (definition, cubeRoot) => definition with { CubeRoot = cubeRoot.AsNamedType() },
        locator: static (locations, cubeRootLocation) => locations with { CubeRoot = cubeRootLocation }
    );

    private static GeneratedScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(GeneratedScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
