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
        UseUnitBias,
        ImplementSum,
        ImplementDifference,
        Difference,
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
        name: nameof(SharpMeasuresScalarAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector.AsNamedType() },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static GeneratedScalarProperty<bool> UseUnitBias { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.UseUnitBias),
        setter: static (definition, useUnitBias) => definition with { UseUnitBias = useUnitBias },
        locator: static (locations, useUnitBiasLocation) => locations with { UseUnitBias = useUnitBiasLocation }
    );

    private static GeneratedScalarProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static GeneratedScalarProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static GeneratedScalarProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static GeneratedScalarProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Reciprocal { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Reciprocal),
        setter: static (definition, reciprocal) => definition with { Reciprocal = reciprocal.AsNamedType() },
        locator: static (locations, reciprocalLocation) => locations with { Reciprocal = reciprocalLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Square { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Square),
        setter: static (definition, square) => definition with { Square = square.AsNamedType() },
        locator: static (locations, squareLocation) => locations with { Square = squareLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> Cube { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Cube),
        setter: static (definition, cube) => definition with { Cube = cube.AsNamedType() },
        locator: static (locations, cubeLocation) => locations with { Cube = cubeLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> SquareRoot { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.SquareRoot),
        setter: static (definition, squareRoot) => definition with { SquareRoot = squareRoot.AsNamedType() },
        locator: static (locations, squareRootLocation) => locations with { SquareRoot = squareRootLocation }
    );

    private static GeneratedScalarProperty<INamedTypeSymbol> CubeRoot { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.CubeRoot),
        setter: static (definition, cubeRoot) => definition with { CubeRoot = cubeRoot.AsNamedType() },
        locator: static (locations, cubeRootLocation) => locations with { CubeRoot = cubeRootLocation }
    );

    private static GeneratedScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
