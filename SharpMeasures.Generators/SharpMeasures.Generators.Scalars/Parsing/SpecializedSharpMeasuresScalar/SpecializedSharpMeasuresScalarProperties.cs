namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSpecializedSharpMeasuresScalarDefinition>> AllProperties => new IAttributeProperty<SymbolicSpecializedSharpMeasuresScalarDefinition>[]
    {
        OriginalQuantity,
        InheritDerivations,
        InheritConstants,
        InheritConversions,
        InheritBases,
        InheritUnits,
        ForwardsCastOperatorBehaviour,
        BackwardsCastOperatorBehaviour,
        Vector,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        GenerateDocumentation
    };

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> OriginalQuantity { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.OriginalScalar),
        setter: static (definition, originalQuantity) => definition with { OriginalQuantity = originalQuantity },
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

    private static SpecializedSharpMeasuresScalarProperty<int> ForwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.ForwardsCastOperatorBehaviour),
        setter: static (definition, forwardsCastOperatorBehaviour) => definition with { ForwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)forwardsCastOperatorBehaviour },
        locator: static (locations, forwardsCastOperatorBehaviourLocation) => locations with { ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<int> BackwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.BackwardsCastOperatorBehaviour),
        setter: static (definition, backwardsCastOperatorBehaviour) => definition with { BackwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)backwardsCastOperatorBehaviour },
        locator: static (locations, backwardsCastOperatorBehaviourLocation) => locations with { BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector },
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
        setter: static (definition, difference) => definition with { Difference = difference },
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

    private static SpecializedSharpMeasuresScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedSharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
