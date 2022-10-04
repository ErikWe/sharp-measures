namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SpecializedSharpMeasuresScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSpecializedSharpMeasuresScalarDefinition>> AllProperties => new IAttributeProperty<SymbolicSpecializedSharpMeasuresScalarDefinition>[]
    {
        OriginalQuantity,
        InheritOperations,
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
        name: nameof(SpecializedScalarQuantityAttribute.OriginalScalar),
        setter: static (definition, originalQuantity) => definition with { OriginalQuantity = originalQuantity },
        locator: static (locations, originalQuantityLocation) => locations with { OriginalQuantity = originalQuantityLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritOperations { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.InheritOperations),
        setter: static (definition, inheritOperations) => definition with { InheritOperations = inheritOperations },
        locator: static (locations, inheritOperationsLocation) => locations with { InheritOperations = inheritOperationsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritConstants { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.InheritConstants),
        setter: static (definition, inheritConstants) => definition with { InheritConstants = inheritConstants },
        locator: static (locations, inheritConstantsLocation) => locations with { InheritConstants = inheritConstantsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritConversions { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.InheritConversions),
        setter: static (definition, inheritConversions) => definition with { InheritConversions = inheritConversions },
        locator: static (locations, inheritConversionsLocation) => locations with { InheritConversions = inheritConversionsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritBases { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.InheritBases),
        setter: static (definition, inheritBases) => definition with { InheritBases = inheritBases },
        locator: static (locations, inheritBasesLocation) => locations with { InheritBases = inheritBasesLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> InheritUnits { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.InheritUnits),
        setter: static (definition, inheritUnits) => definition with { InheritUnits = inheritUnits },
        locator: static (locations, inheritUnitsLocation) => locations with { InheritUnits = inheritUnitsLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<int> ForwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.ForwardsCastOperatorBehaviour),
        setter: static (definition, forwardsCastOperatorBehaviour) => definition with { ForwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)forwardsCastOperatorBehaviour },
        locator: static (locations, forwardsCastOperatorBehaviourLocation) => locations with { ForwardsCastOperatorBehaviour = forwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<int> BackwardsCastOperatorBehaviour { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.BackwardsCastOperatorBehaviour),
        setter: static (definition, backwardsCastOperatorBehaviour) => definition with { BackwardsCastOperatorBehaviour = (ConversionOperatorBehaviour)backwardsCastOperatorBehaviour },
        locator: static (locations, backwardsCastOperatorBehaviourLocation) => locations with { BackwardsCastOperatorBehaviour = backwardsCastOperatorBehaviourLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SpecializedSharpMeasuresScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SpecializedScalarQuantityAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
