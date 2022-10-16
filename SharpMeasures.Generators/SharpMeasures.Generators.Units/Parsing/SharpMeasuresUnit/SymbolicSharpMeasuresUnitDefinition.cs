namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSharpMeasuresUnitDefinition : ARawAttributeDefinition<SymbolicSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>
{
    public static SymbolicSharpMeasuresUnitDefinition Empty { get; } = new(SharpMeasuresUnitLocations.Empty);

    protected override SymbolicSharpMeasuresUnitDefinition Definition => this;

    public INamedTypeSymbol? Quantity { get; init; }

    public bool BiasTerm { get; init; }

    private SymbolicSharpMeasuresUnitDefinition(SharpMeasuresUnitLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (Quantity is not null && (alreadyInForeignAssembly || Quantity.ContainingAssembly.Name != localAssemblyName))
        {
            yield return Quantity;
        }
    }
}
