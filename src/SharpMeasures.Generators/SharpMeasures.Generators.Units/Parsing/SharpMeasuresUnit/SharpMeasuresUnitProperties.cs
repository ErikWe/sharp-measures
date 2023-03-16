namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal static class SharpMeasuresUnitProperties
{
    public static IReadOnlyList<IAttributeProperty<SymbolicSharpMeasuresUnitDefinition>> AllProperties => new IAttributeProperty<SymbolicSharpMeasuresUnitDefinition>[]
    {
        Quantity,
        BiasTerm
    };

    private static SharpMeasuresUnitProperty<INamedTypeSymbol> Quantity { get; } = new
    (
        name: nameof(UnitAttribute.Quantity),
        setter: static (definition, quantity) => definition with { Quantity = quantity },
        locator: static (locations, quantityLocation) => locations with { Quantity = quantityLocation }
    );

    private static SharpMeasuresUnitProperty<bool> BiasTerm { get; } = new
    (
        name: nameof(UnitAttribute.BiasTerm),
        setter: static (definition, biasTerm) => definition with { BiasTerm = biasTerm },
        locator: static (locations, biasTermLocation) => locations with { BiasTerm = biasTermLocation }
    );
}
