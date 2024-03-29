﻿namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class SymbolicSpecializedSharpMeasuresVectorGroupDefinition : ARawAttributeDefinition<SymbolicSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations>
{
    public static SymbolicSpecializedSharpMeasuresVectorGroupDefinition Empty => new(SpecializedSharpMeasuresVectorGroupLocations.Empty);

    public INamedTypeSymbol? OriginalQuantity { get; init; }

    public bool InheritOperations { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public ConversionOperatorBehaviour ForwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Explicit;
    public ConversionOperatorBehaviour BackwardsCastOperatorBehaviour { get; init; } = ConversionOperatorBehaviour.Implicit;

    public INamedTypeSymbol? Scalar { get; init; }

    public bool? ImplementSum { get; init; }
    public bool? ImplementDifference { get; init; }
    public INamedTypeSymbol? Difference { get; init; }

    public string? DefaultUnitInstanceName { get; init; }
    public string? DefaultUnitInstanceSymbol { get; init; }

    protected override SymbolicSpecializedSharpMeasuresVectorGroupDefinition Definition => this;

    private SymbolicSpecializedSharpMeasuresVectorGroupDefinition(SpecializedSharpMeasuresVectorGroupLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        if (isForeign(OriginalQuantity))
        {
            yield return OriginalQuantity!;
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
