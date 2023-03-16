namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Vectors.Parsing.VectorOperation;

using System.Collections.Generic;

internal sealed record class VectorOperationProcessingContext : IVectorOperationProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<(string, NamedType)> ReservedMethodSignatures { get; }

    public VectorOperationProcessingContext(DefinedType type, HashSet<(string, NamedType)> reservedMethodSignatures)
    {
        Type = type;

        ReservedMethodSignatures = reservedMethodSignatures;
    }
}
