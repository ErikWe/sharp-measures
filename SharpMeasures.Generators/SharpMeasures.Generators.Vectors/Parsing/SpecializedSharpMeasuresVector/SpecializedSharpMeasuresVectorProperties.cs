namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSpecializedSharpMeasuresVectorDefinition>> AllProperties => new IAttributeProperty<SymbolicSpecializedSharpMeasuresVectorDefinition>[]
    {
        OriginalVector,
        InheritDerivations,
        InheritConstants,
        InheritConversions,
        InheritUnits,
        Scalar,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        GenerateDocumentation
    };

    private static SpecializedSharpMeasuresVectorProperty<INamedTypeSymbol> OriginalVector { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.OriginalVector),
        setter: static (definition, originalQuantity) => definition with { OriginalQuantity = originalQuantity },
        locator: static (locations, originalQuantityLocation) => locations with { OriginalQuantity = originalQuantityLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> InheritDerivations { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.InheritDerivations),
        setter: static (definition, inheritDerivations) => definition with { InheritDerivations = inheritDerivations },
        locator: static (locations, inheritDerivationsLocation) => locations with { InheritDerivations = inheritDerivationsLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> InheritConstants { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.InheritConstants),
        setter: static (definition, inheritConstants) => definition with { InheritConstants = inheritConstants },
        locator: static (locations, inheritConstantsLocation) => locations with { InheritConstants = inheritConstantsLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
