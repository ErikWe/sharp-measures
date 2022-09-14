﻿namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

internal record class SymbolicSpecializedSharpMeasuresScalarDefinition : ARawAttributeDefinition<SymbolicSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations>
{
    public static SymbolicSpecializedSharpMeasuresScalarDefinition Empty => new(SpecializedSharpMeasuresScalarLocations.Empty);

    public INamedTypeSymbol? OriginalQuantity { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConstants { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritBases { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public INamedTypeSymbol? Vector { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    public INamedTypeSymbol? Reciprocal { get; init; }
    public INamedTypeSymbol? Square { get; init; }
    public INamedTypeSymbol? Cube { get; init; }
    public INamedTypeSymbol? SquareRoot { get; init; }
    public INamedTypeSymbol? CubeRoot { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override SymbolicSpecializedSharpMeasuresScalarDefinition Definition => this;

    private SymbolicSpecializedSharpMeasuresScalarDefinition(SpecializedSharpMeasuresScalarLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName) => new[] { OriginalQuantity, Vector, Difference, Reciprocal, Square, Cube, SquareRoot, CubeRoot }.Where((symbol) => symbol is not null && symbol.ContainingAssembly.Name != localAssemblyName).Select(static (symbol) => symbol!);
}
