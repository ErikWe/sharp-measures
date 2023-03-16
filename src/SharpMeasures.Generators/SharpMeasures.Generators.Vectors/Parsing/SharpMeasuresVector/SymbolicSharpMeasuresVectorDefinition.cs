namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSharpMeasuresVectorDefinition : ARawAttributeDefinition<SymbolicSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations>
{
    public static SymbolicSharpMeasuresVectorDefinition Empty => new(SharpMeasuresVectorLocations.Empty);

    public INamedTypeSymbol? Unit { get; init; }
    public INamedTypeSymbol? Scalar { get; init; }

    public int? Dimension { get; init; }

    public bool ImplementSum { get; init; } = true;
    public bool ImplementDifference { get; init; } = true;
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    protected override SymbolicSharpMeasuresVectorDefinition Definition => this;

    private SymbolicSharpMeasuresVectorDefinition(SharpMeasuresVectorLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (isForeign(Unit))
        {
            yield return Unit!;
        }

        if (isForeign(Scalar))
        {
            yield return Scalar!;
        }

        if (isForeign(Difference))
        {
            yield return Difference!;
        }

        bool isForeign(INamedTypeSymbol? symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName);
    }
}
