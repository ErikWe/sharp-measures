namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresVectorGroupProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSharpMeasuresVectorGroupDefinition>> AllProperties => new IAttributeProperty<SymbolicSharpMeasuresVectorGroupDefinition>[]
    {
        Unit,
        Scalar,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(VectorGroupAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(VectorGroupAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(VectorGroupAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(VectorGroupAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(VectorGroupAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SharpMeasuresVectorGroupProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(VectorGroupAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SharpMeasuresVectorGroupProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(VectorGroupAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(VectorGroupAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
