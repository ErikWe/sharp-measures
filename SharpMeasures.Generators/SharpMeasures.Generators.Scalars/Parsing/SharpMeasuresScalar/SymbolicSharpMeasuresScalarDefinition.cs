namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSharpMeasuresScalarDefinition : ARawAttributeDefinition<SymbolicSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations>
{
    public static SymbolicSharpMeasuresScalarDefinition Empty => new();

    public INamedTypeSymbol? Unit { get; init; }
    public INamedTypeSymbol? Vector { get; init; }

    public bool UseUnitBias { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    protected override SymbolicSharpMeasuresScalarDefinition Definition => this;

    private SymbolicSharpMeasuresScalarDefinition() : base(SharpMeasuresScalarLocations.Empty) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (isForeign(Unit))
        {
            yield return Unit!;
        }

        if (isForeign(Vector))
        {
            yield return Vector!;
        }

        if (isForeign(Difference))
        {
            yield return Difference!;
        }

        bool isForeign(INamedTypeSymbol? symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName);
    }
}
