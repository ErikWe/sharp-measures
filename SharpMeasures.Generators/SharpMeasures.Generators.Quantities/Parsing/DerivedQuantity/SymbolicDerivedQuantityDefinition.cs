namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System;
using System.Collections.Generic;
using System.Linq;

public record class SymbolicDerivedQuantityDefinition : ARawAttributeDefinition<SymbolicDerivedQuantityDefinition, DerivedQuantityLocations>
{
    public static SymbolicDerivedQuantityDefinition Empty { get; } = new(DerivedQuantityLocations.Empty);

    public string? Expression { get; init; }
    public IReadOnlyList<INamedTypeSymbol?> Signature
    {
        get => signature;
        init => signature = value.AsReadOnlyEquatable();
    }

    public DerivationOperatorImplementation OperatorImplementation { get; init; } = DerivationOperatorImplementation.Suitable;

    public bool Permutations { get; init; }

    private ReadOnlyEquatableList<INamedTypeSymbol?> signature { get; init; } = ReadOnlyEquatableList<INamedTypeSymbol?>.Empty;

    protected override SymbolicDerivedQuantityDefinition Definition => this;

    private SymbolicDerivedQuantityDefinition(DerivedQuantityLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (Signature is null)
        {
            return Array.Empty<INamedTypeSymbol>();
        }

        return Signature.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
    }
}
