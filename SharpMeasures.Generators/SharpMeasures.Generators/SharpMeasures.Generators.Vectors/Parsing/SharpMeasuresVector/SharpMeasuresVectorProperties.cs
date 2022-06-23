namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSharpMeasuresVectorDefinition>> AllProperties => new IAttributeProperty<RawSharpMeasuresVectorDefinition>[]
    {
        Unit,
        Scalar,
        Dimension,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static SharpMeasuresVectorProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static SharpMeasuresVectorProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static SharpMeasuresVectorProperty<int> Dimension { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static SharpMeasuresVectorProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SharpMeasuresVectorProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SharpMeasuresVectorProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SharpMeasuresVectorProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static SharpMeasuresVectorProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static SharpMeasuresVectorProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
