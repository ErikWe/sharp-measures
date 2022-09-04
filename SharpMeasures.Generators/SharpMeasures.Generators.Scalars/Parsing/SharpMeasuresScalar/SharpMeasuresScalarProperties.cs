namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSharpMeasuresScalarDefinition>> AllProperties => new IAttributeProperty<RawSharpMeasuresScalarDefinition>[]
    {
        Unit,
        Vector,
        UseUnitBias,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        Reciprocal,
        Square,
        Cube,
        SquareRoot,
        CubeRoot,
        GenerateDocumentation
    };

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector.AsNamedType() },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static SharpMeasuresScalarProperty<bool> UseUnitBias { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.UseUnitBias),
        setter: static (definition, useUnitBias) => definition with { UseUnitBias = useUnitBias },
        locator: static (locations, useUnitBiasLocation) => locations with { UseUnitBias = useUnitBiasLocation }
    );

    private static SharpMeasuresScalarProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SharpMeasuresScalarProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SharpMeasuresScalarProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SharpMeasuresScalarProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Reciprocal { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Reciprocal),
        setter: static (definition, reciprocal) => definition with { Reciprocal = reciprocal.AsNamedType() },
        locator: static (locations, reciprocalLocation) => locations with { Reciprocal = reciprocalLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Square { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Square),
        setter: static (definition, square) => definition with { Square = square.AsNamedType() },
        locator: static (locations, squareLocation) => locations with { Square = squareLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Cube { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Cube),
        setter: static (definition, cube) => definition with { Cube = cube.AsNamedType() },
        locator: static (locations, cubeLocation) => locations with { Cube = cubeLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> SquareRoot { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.SquareRoot),
        setter: static (definition, squareRoot) => definition with { SquareRoot = squareRoot.AsNamedType() },
        locator: static (locations, squareRootLocation) => locations with { SquareRoot = squareRootLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> CubeRoot { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.CubeRoot),
        setter: static (definition, cubeRoot) => definition with { CubeRoot = cubeRoot.AsNamedType() },
        locator: static (locations, cubeRootLocation) => locations with { CubeRoot = cubeRootLocation }
    );

    private static SharpMeasuresScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
