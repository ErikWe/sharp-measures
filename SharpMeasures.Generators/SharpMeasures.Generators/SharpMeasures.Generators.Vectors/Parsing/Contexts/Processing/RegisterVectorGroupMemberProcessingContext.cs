namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using System.Collections.Generic;

internal record class RegisterVectorGroupMemberProcessingContext : IRegisterVectorGroupMemberProcessingContext
{
    public DefinedType Type { get; }

    public HashSet<int> ReservedDimensions { get; } = new();

    public RegisterVectorGroupMemberProcessingContext(DefinedType type)
    {
        Type = type;
    }
}
