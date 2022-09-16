namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

internal record class SymbolicSpecializedSharpMeasuresVectorDefinition : ARawAttributeDefinition<SymbolicSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations>
{
    public static SymbolicSpecializedSharpMeasuresVectorDefinition Empty => new(SpecializedSharpMeasuresVectorLocations.Empty);

    public INamedTypeSymbol? OriginalQuantity { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public INamedTypeSymbol? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override SymbolicSpecializedSharpMeasuresVectorDefinition Definition => this;

    private SymbolicSpecializedSharpMeasuresVectorDefinition(SpecializedSharpMeasuresVectorLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly) => new[] { OriginalQuantity, Scalar, Difference }.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
}
