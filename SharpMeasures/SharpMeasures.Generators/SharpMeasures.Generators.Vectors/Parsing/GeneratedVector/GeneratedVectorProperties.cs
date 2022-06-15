namespace SharpMeasures.Generators.Vectors.Parsing.GeneratedVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class GeneratedVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawGeneratedVectorDefinition>> AllProperties => new IAttributeProperty<RawGeneratedVectorDefinition>[]
    {
        Unit,
        Scalar,
        Dimension,
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static GeneratedVectorProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static GeneratedVectorProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static GeneratedVectorProperty<int> Dimension { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
    );

    private static GeneratedVectorProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static GeneratedVectorProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static GeneratedVectorProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresVectorAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
