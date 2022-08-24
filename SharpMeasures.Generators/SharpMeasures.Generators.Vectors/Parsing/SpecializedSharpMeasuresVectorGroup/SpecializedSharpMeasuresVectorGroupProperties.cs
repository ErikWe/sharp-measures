namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresVectorGroupProperties
{
    public static IReadOnlyList<IAttributeProperty<RawSpecializedSharpMeasuresVectorGroupDefinition>> AllProperties
        => new IAttributeProperty<RawSpecializedSharpMeasuresVectorGroupDefinition>[]
    {
        OriginalVectorGroup,
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

    private static SpecializedSharpMeasuresVectorGroupProperty<INamedTypeSymbol> OriginalVectorGroup { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.OriginalVectorGroup),
        setter: static (definition, originalVectorGroup) => definition with { OriginalVectorGroup = originalVectorGroup.AsNamedType() },
        locator: static (locations, originalVectorGroupLocation) => locations with { OriginalVectorGroup = originalVectorGroupLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> InheritDerivations { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.InheritDerivations),
        setter: static (definition, inheritDerivations) => definition with { InheritDerivations = inheritDerivations },
        locator: static (locations, inheritDerivationsLocation) => locations with { InheritDerivations = inheritDerivationsLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> InheritConstants { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.InheritConstants),
        setter: static (definition, inheritConstants) => definition with { InheritConstants = inheritConstants },
        locator: static (locations, inheritConstantsLocation) => locations with { InheritConstants = inheritConstantsLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar.AsNamedType() },
        locator: static (locations, scalarLocation) => locations with { Scalar = scalarLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference.AsNamedType() },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<string> DefaultUnitName { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.DefaultUnitName),
        setter: static (definition, defaultUnitName) => definition with { DefaultUnitName = defaultUnitName },
        locator: static (locations, defaultUnitNameLocation) => locations with { DefaultUnitName = defaultUnitNameLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<string> DefaultUnitSymbol { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.DefaultUnitSymbol),
        setter: static (definition, defaultUnitSymbol) => definition with { DefaultUnitSymbol = defaultUnitSymbol },
        locator: static (locations, defaultUnitSymbolLocation) => locations with { DefaultUnitSymbol = defaultUnitSymbolLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
