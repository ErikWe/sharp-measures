namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresVectorGroupProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSpecializedSharpMeasuresVectorGroupDefinition>> AllProperties => new IAttributeProperty<SymbolicSpecializedSharpMeasuresVectorGroupDefinition>[]
    {
        OriginalVectorGroup,
        InheritDerivations,
        InheritConstants,
        InheritConversions,
        InheritUnits,
        ForwardsCastOperatorBehaviour,
        BackwardsCastOperatorBehaviour,
        Scalar,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        GenerateDocumentation
    };

    private static SpecializedSharpMeasuresVectorGroupProperty<INamedTypeSymbol> OriginalVectorGroup { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.OriginalVectorGroup),
        setter: static (definition, originalQuantity) => definition with { OriginalQuantity = originalQuantity },
        locator: static (locations, originalQuantityLocation) => locations with { OriginalQuantity = originalQuantityLocation }
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

    private static SpecializedSharpMeasuresVectorGroupProperty<int> ForwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.ForwardsCastOperatorBehaviour),
        setter: static (definition, forwardsCastOperatorBehaviour) => definition with { ForwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)forwardsCastOperatorBehaviour },
        locator: static (locations, forwardsCastOperatorBehaviourLocation) => locations with { ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<int> BackwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.BackwardsCastOperatorBehaviour),
        setter: static (definition, backwardsCastOperatorBehaviour) => definition with { BackwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)backwardsCastOperatorBehaviour },
        locator: static (locations, backwardsCastOperatorBehaviourLocation) => locations with { BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<INamedTypeSymbol> Scalar { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.Scalar),
        setter: static (definition, scalar) => definition with { Scalar = scalar },
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
        setter: static (definition, difference) => definition with { Difference = difference },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SpecializedSharpMeasuresVectorGroupProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresVectorGroupAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
