namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresVectorGroupProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSharpMeasuresVectorGroupDefinition>> AllProperties => new IAttributeProperty<RawSharpMeasuresVectorGroupDefinition>[]
    {
        Unit,
        Scalar,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SharpMeasuresVectorGroupProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SharpMeasuresVectorGroupProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static SharpMeasuresVectorGroupProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static SharpMeasuresVectorGroupProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresVectorGroupAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
