namespace SharpMeasures.Generators.Vectors.Parsing.Contexts.Resolution;

using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

internal record class RegisterVectorGroupMemberResolutionContext : IRegisterVectorGroupMemberResolutionContext
{
    public DefinedType Type { get; }

    public IUnresolvedVectorGroupType VectorGroup { get; }
    public IUnresolvedVectorPopulation VectorPopulation { get; }

    public RegisterVectorGroupMemberResolutionContext(DefinedType type, IUnresolvedVectorGroupType vectorGroup, IUnresolvedVectorPopulation vectorPopulation)
    {
        Type = type;

        VectorGroup = vectorGroup;
        VectorPopulation = vectorPopulation;
    }
}
