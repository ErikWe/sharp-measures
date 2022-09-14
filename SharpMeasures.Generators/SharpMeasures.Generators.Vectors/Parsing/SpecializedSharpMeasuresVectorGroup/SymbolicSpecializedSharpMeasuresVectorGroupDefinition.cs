namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

internal record class SymbolicSpecializedSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<SymbolicSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations>
{
    public static SymbolicSpecializedSharpMeasuresVectorGroupDefinition Empty => new(SpecializedSharpMeasuresVectorGroupLocations.Empty);

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

    protected override SymbolicSpecializedSharpMeasuresVectorGroupDefinition Definition => this;

    private SymbolicSpecializedSharpMeasuresVectorGroupDefinition(SpecializedSharpMeasuresVectorGroupLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName) => new[] { OriginalQuantity, Scalar, Difference }.Where((symbol) => symbol is not null && symbol.ContainingAssembly.Name != localAssemblyName).Select(static (symbol) => symbol!);
}
