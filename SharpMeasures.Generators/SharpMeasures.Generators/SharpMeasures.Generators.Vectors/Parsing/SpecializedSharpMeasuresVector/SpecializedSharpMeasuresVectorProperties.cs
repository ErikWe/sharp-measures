namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Scalars;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresVectorProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSpecializedSharpMeasuresVectorDefinition>> AllProperties
        => new IAttributeProperty<RawSpecializedSharpMeasuresVectorDefinition>[]
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
        DefaultUnitName,
        DefaultUnitSymbol,
        GenerateDocumentation
    };

    private static SpecializedSharpMeasuresVectorProperty<INamedTypeSymbol> OriginalVector { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorAttribute.OriginalVector),
        setter: static (definition, originalVector) => definition with { OriginalVector = originalVector.AsNamedType() },
        locator: static (locations, originalVectorLocation) => locations with { OriginalVector = originalVectorLocation }
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
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
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
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static SpecializedSharpMeasuresVectorProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
