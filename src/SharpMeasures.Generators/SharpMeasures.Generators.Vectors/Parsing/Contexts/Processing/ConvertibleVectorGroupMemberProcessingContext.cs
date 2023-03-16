namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Vectors.Parsing.ConvertibleVector;

using System.Collections.Generic;

internal sealed record class ConvertibleVectorGroupMemberProcessingContext : IConvertibleVectorGroupMemberProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<NamedType> ListedIncomingConversions { get; } = new();
    public HashSet<NamedType> ListedOutgoingConversions { get; } = new();

    public NamedType? OriginalQuantity { get; }

    public bool ConversionFromOriginalQuantitySpecified { get; }
    public bool ConversionToOriginalQuantitySpecified { get; }

    public NamedType Group { get; }

    public ConvertibleVectorGroupMemberProcessingContext(DefinedType type, NamedType group)
    {
        Type = type;

        Group = group;
    }
}
