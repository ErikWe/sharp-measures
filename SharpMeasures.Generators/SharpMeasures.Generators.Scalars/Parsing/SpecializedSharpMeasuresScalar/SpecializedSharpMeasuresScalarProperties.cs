namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSpecializedSharpMeasuresScalarDefinition>> AllProperties
        => new IAttributeProperty<RawSpecializedSharpMeasuresScalarDefinition>[]
    {
        OriginalQuantity,
        InheritDerivations,
        InheritConstants,
        InheritConversions,
        InheritBases,
        InheritUnits,
        Vector,
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

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> OriginalQuantity { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.OriginalScalar),
        setter: static (definition, originalQuantity) => definition with { OriginalQuantity = originalQuantity.AsNamedType() },
        locator: static (locations, originalQuantityLocation) => locations with { OriginalQuantity = originalQuantityLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritDerivations { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.InheritDerivations),
        setter: static (definition, inheritDerivations) => definition with { InheritDerivations = inheritDerivations },
        locator: static (locations, inheritDerivationsLocation) => locations with { InheritDerivations = inheritDerivationsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritConstants { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.InheritConstants),
        setter: static (definition, inheritConstants) => definition with { InheritConstants = inheritConstants },
        locator: static (locations, inheritConstantsLocation) => locations with { InheritConstants = inheritConstantsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritBases { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.InheritBases),
        setter: static (definition, inheritBases) => definition with { InheritBases = inheritBases },
        locator: static (locations, inheritBasesLocation) => locations with { InheritBases = inheritBasesLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector.AsNamedType() },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Reciprocal { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Reciprocal),
        setter: static (definition, reciprocal) => definition with { Reciprocal = reciprocal.AsNamedType() },
        locator: static (locations, reciprocalLocation) => locations with { Reciprocal = reciprocalLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Square { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Square),
        setter: static (definition, square) => definition with { Square = square.AsNamedType() },
        locator: static (locations, squareLocation) => locations with { Square = squareLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Cube { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Cube),
        setter: static (definition, cube) => definition with { Cube = cube.AsNamedType() },
        locator: static (locations, cubeLocation) => locations with { Cube = cubeLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> SquareRoot { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.SquareRoot),
        setter: static (definition, squareRoot) => definition with { SquareRoot = squareRoot.AsNamedType() },
        locator: static (locations, squareRootLocation) => locations with { SquareRoot = squareRootLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> CubeRoot { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.CubeRoot),
        setter: static (definition, cubeRoot) => definition with { CubeRoot = cubeRoot.AsNamedType() },
        locator: static (locations, cubeRootLocation) => locations with { CubeRoot = cubeRootLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
