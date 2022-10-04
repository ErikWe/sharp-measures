namespace SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

internal sealed record class SymbolicVectorOperationDefinition : ARawAttributeDefinition<SymbolicVectorOperationDefinition, VectorOperationLocations>
{
    public static SymbolicVectorOperationDefinition Empty { get; } = new(VectorOperationLocations.Empty);

    public INamedTypeSymbol? Result { get; init; }
    public INamedTypeSymbol? Other { get; init; }

    public VectorOperatorType OperatorType { get; init; }
    public OperatorPosition Position { get; init; } = OperatorPosition.Left;

    public bool Mirror { get; init; }

    public string? Name { get; init; }
    public string? MirroredName { get; init; }

    protected override SymbolicVectorOperationDefinition Definition => this;

    private SymbolicVectorOperationDefinition(VectorOperationLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        return new[] { Result, Other }.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
    }
}
