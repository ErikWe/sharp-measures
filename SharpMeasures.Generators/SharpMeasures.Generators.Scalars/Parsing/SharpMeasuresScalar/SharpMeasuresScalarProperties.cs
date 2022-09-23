namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresScalarProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSharpMeasuresScalarDefinition>> AllProperties => new IAttributeProperty<SymbolicSharpMeasuresScalarDefinition>[]
    {
        Unit,
        Vector,
        UseUnitBias,
        ImplementSum,
        ImplementDifference,
        Difference,
        DefaultUnitInstanceName,
        DefaultUnitInstanceSymbol,
        GenerateDocumentation
    };

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Unit { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Unit),
        setter: static (definition, unit) => definition with { Unit = unit },
        locator: static (locations, unitLocation) => locations with { Unit = unitLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Vector { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Vector),
        setter: static (definition, vector) => definition with { Vector = vector },
        locator: static (locations, vectorLocation) => locations with { Vector = vectorLocation }
    );

    private static SharpMeasuresScalarProperty<bool> UseUnitBias { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.UseUnitBias),
        setter: static (definition, useUnitBias) => definition with { UseUnitBias = useUnitBias },
        locator: static (locations, useUnitBiasLocation) => locations with { UseUnitBias = useUnitBiasLocation }
    );

    private static SharpMeasuresScalarProperty<bool> ImplementSum { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementSum),
        setter: static (definition, implementSum) => definition with { ImplementSum = implementSum },
        locator: static (locations, implementSumLocation) => locations with { ImplementSum = implementSumLocation }
    );

    private static SharpMeasuresScalarProperty<bool> ImplementDifference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.ImplementDifference),
        setter: static (definition, implementDifference) => definition with { ImplementDifference = implementDifference },
        locator: static (locations, implementDifferenceLocation) => locations with { ImplementDifference = implementDifferenceLocation }
    );

    private static SharpMeasuresScalarProperty<INamedTypeSymbol> Difference { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.Difference),
        setter: static (definition, difference) => definition with { Difference = difference },
        locator: static (locations, differenceLocation) => locations with { Difference = differenceLocation }
    );

    private static SharpMeasuresScalarProperty<string> DefaultUnitInstanceName { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitInstanceName),
        setter: static (definition, defaultUnitInstanceName) => definition with { DefaultUnitInstanceName = defaultUnitInstanceName },
        locator: static (locations, defaultUnitInstanceNameLocation) => locations with { DefaultUnitInstanceName = defaultUnitInstanceNameLocation }
    );

    private static SharpMeasuresScalarProperty<string> DefaultUnitInstanceSymbol { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.DefaultUnitInstanceSymbol),
        setter: static (definition, defaultUnitInstanceSymbol) => definition with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbol },
        locator: static (locations, defaultUnitInstanceSymbolLocation) => locations with { DefaultUnitInstanceSymbol = defaultUnitInstanceSymbolLocation }
    );

    private static SharpMeasuresScalarProperty<bool> GenerateDocumentation { get; } = new
    (
        name: nameof(SharpMeasuresScalarAttribute.GenerateDocumentation),
        setter: static (definition, generateDocumentation) => definition with { GenerateDocumentation = generateDocumentation },
        locator: static (locations, generateDocumentationLocation) => locations with { GenerateDocumentation = generateDocumentationLocation }
    );
}
