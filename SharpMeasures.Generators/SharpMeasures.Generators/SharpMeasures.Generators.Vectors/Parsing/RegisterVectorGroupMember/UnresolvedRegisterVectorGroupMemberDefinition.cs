namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class UnresolvedRegisterVectorGroupMemberDefinition : AAttributeDefinition<RegisterVectorGroupMemberLocations>, IUnresolvedRegisteredVectorGroupMember
{
    public NamedType Vector { get; }

    public int Dimension { get; }

    public UnresolvedRegisterVectorGroupMemberDefinition(NamedType vector, int dimension, RegisterVectorGroupMemberLocations locations)
        : base(locations)
    {
        Vector = vector;

        Dimension = dimension;
    }
}
