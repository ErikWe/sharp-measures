namespace SharpMeasures.Generators.Quantities.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using System.Collections.Generic;

public sealed class QuantityOperationProcessingContext : IQuantityOperationProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<(OperatorType, NamedType)> ReservedLHSOperators { get; } = new();
    public HashSet<(OperatorType, NamedType)> ReservedRHSOperators { get; } = new();
    public HashSet<(string, NamedType)> ReservedMethodSignatures { get; } = new();

    public QuantityOperationProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
