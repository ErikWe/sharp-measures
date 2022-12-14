namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using Microsoft.CodeAnalysis;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicDerivableUnitDefinition : ARawAttributeDefinition<SymbolicDerivableUnitDefinition, DerivableUnitLocations>
{
    public static SymbolicDerivableUnitDefinition Empty { get; } = new(DerivableUnitLocations.Empty);

    public string? DerivationID { get; init; }
    public string? Expression { get; init; }
    public IReadOnlyList<INamedTypeSymbol?>? Signature
    {
        get => signatureField;
        init => signatureField = value?.AsReadOnlyEquatable();
    }

    public bool Permutations { get; init; }

    private readonly IReadOnlyList<INamedTypeSymbol?>? signatureField;

    protected override SymbolicDerivableUnitDefinition Definition => this;

    private SymbolicDerivableUnitDefinition(DerivableUnitLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (Signature is null)
        {
            yield break;
        }

        foreach (var symbol in Signature)
        {
            if (symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName))
            {
                yield return symbol;
            }
        }
    }
}
