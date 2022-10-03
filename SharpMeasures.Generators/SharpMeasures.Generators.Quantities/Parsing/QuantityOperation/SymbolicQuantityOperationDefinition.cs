namespace SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;
using System.Linq;

public sealed record class SymbolicQuantityOperationDefinition : ARawAttributeDefinition<SymbolicQuantityOperationDefinition, QuantityOperationLocations>
{
    public static SymbolicQuantityOperationDefinition Empty { get; } = new(QuantityOperationLocations.Empty);

    public INamedTypeSymbol? Result { get; init; }
    public INamedTypeSymbol? Other { get; init; }

    public OperatorType OperatorType { get; init; }
    public OperatorPosition Position { get; init; } = OperatorPosition.Left;

    public bool Mirror { get; init; }

    public QuantityOperationImplementation Implementation { get; init; } = QuantityOperationImplementation.OperatorAndMethod;

    public string? MethodName { get; init; }
    public string? MirroredMethodName { get; init; }

    protected override SymbolicQuantityOperationDefinition Definition => this;

    private SymbolicQuantityOperationDefinition(QuantityOperationLocations locations) : base(locations) { }

    public IEnumerable<INamedTypeSymbol> ForeignSymbols(string localAssemblyName, bool alreadyInForeignAssembly)
    {
        return new[] { Result, Other }.Where((symbol) => symbol is not null && (alreadyInForeignAssembly || symbol.ContainingAssembly.Name != localAssemblyName)).Select(static (symbol) => symbol!);
    }
}
