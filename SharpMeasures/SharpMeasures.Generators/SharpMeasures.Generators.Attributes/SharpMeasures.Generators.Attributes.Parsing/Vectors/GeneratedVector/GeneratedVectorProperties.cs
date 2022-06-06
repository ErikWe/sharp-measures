namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Vectors;

using System.Collections.Generic;

internal static class GeneratedVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawGeneratedVector>> AllProperties => new IAttributeProperty<RawGeneratedVector>[]
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
        name: nameof(GeneratedVectorAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit.AsNamedType() },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static GeneratedVectorProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static GeneratedVectorProperty<int> Dimension { get; } = new
    (
        name: nameof(GeneratedVectorAttribute.Dimension),
        setter: static (definition, dimension) => definition with { Dimension = dimension },
        locator: static (locations, dimensionLocation) => locations with { Dimension = dimensionLocation }
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
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
