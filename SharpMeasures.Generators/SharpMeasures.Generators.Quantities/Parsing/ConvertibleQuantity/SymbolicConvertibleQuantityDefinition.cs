namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System;
using System.Collections.Generic;
using System.Linq;

public record class SymbolicConvertibleQuantityDefinition : ARawItemListDefinition<INamedTypeSymbol?, SymbolicConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    internal static SymbolicConvertibleQuantityDefinition Empty => new(ConvertibleQuantityLocations.Empty);

    public IReadOnlyList<INamedTypeSymbol?> Quantities => Items;

    public QuantityConversionDirection ConversionDirection { get; init; } = QuantityConversionDirection.Onedirectional;
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;

    protected override SymbolicConvertibleQuantityDefinition Definition => this;

    protected SymbolicConvertibleQuantityDefinition(ConvertibleQuantityLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (Quantities is null)
        {
            return Array.Empty<INamedTypeSymbol>();
        }

        return Quantities.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
    }
}
