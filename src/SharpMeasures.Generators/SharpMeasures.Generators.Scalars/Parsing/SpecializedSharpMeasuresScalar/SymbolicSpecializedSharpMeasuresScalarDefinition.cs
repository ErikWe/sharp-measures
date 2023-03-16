namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSpecializedSharpMeasuresScalarDefinition : ARawAttributeDefinition<SymbolicSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>
{
    public static SymbolicSpecializedSharpMeasuresScalarDefinition Empty => new(SpecializedSharpMeasuresScalarLocations.Empty);

    public INamedTypeSymbol? OriginalQuantity { get; init; }

    public bool InheritOperations { get; init; } = true;
    public bool InheritProcesses { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritBases { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Implicit;

    public INamedTypeSymbol? Vector { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    protected override SymbolicSpecializedSharpMeasuresScalarDefinition Definition => this;

    private SymbolicSpecializedSharpMeasuresScalarDefinition(SpecializedSharpMeasuresScalarLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (isForeign(OriginalQuantity))
        {
            yield return OriginalQuantity!;
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
