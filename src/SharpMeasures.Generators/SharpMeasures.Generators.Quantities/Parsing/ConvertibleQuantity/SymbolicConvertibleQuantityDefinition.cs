namespace SharpMeasures.Generators.Quantities.Parsing.ConvertibleQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing.ItemLists;

using System.Collections.Generic;

public sealed record class SymbolicConvertibleQuantityDefinition : ARawItemListDefinition<INamedTypeSymbol?, SymbolicConvertibleQuantityDefinition, ConvertibleQuantityLocations>
{
    internal static SymbolicConvertibleQuantityDefinition Empty => new(ConvertibleQuantityLocations.Empty);

    public IReadOnlyList<INamedTypeSymbol?> Quantities => Items;

    public QuantityConversionDirection ConversionDirection { get; init; } = QuantityConversionDirection.Onedirectional;
    public ConversionOperatorBehaviour CastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;

    protected override SymbolicConvertibleQuantityDefinition Definition => this;

    private SymbolicConvertibleQuantityDefinition(ConvertibleQuantityLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        foreach (var symbol in Quantities)
        {
            if (symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName))
            {
                yield return symbol;
            }
        }
    }
}
