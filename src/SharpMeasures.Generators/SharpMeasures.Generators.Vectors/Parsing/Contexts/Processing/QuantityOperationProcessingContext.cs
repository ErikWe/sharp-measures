namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Quantities.Parsing.QuantityOperation;

using System.Collections.Generic;

internal sealed record class QuantityOperationProcessingContext : IQuantityOperationProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<(OperatorType, NamedType)> ReservedLHSOperators { get; } = new();
    public HashSet<(OperatorType, NamedType)> ReservedRHSOperators { get; } = new();
    public HashSet<(string, NamedType)> ReservedMethodSignatures { get; }

    public QuantityOperationProcessingContext(DefinedType type, HashSet<(string, NamedType)> reservedMethodSignatures)
    {
        Type = type;

        ReservedMethodSignatures = reservedMethodSignatures;
    }
}
