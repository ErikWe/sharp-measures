namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal sealed record class ConvertibleVectorGroupMemberProcessingContext : IConvertibleVectorGroupMemberProcessingContext
{
    public DefinedType Type { get; }

    public NamedType Group { get; }

    public HashSet<NamedType> ListedQuantities { get; } = new();

    public ConvertibleVectorGroupMemberProcessingContext(DefinedType type, NamedType group)
    {
        Type = type;

        Group = group;
    }
}
