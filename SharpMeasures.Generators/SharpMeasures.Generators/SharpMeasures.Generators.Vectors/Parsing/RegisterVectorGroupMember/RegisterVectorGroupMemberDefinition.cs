namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class RegisterVectorGroupMemberDefinition : AAttributeDefinition<RegisterVectorGroupMemberLocations>, IRegisteredVectorGroupMember
{
    public IUnresolvedVectorGroupMemberType Vector { get; }

    public int Dimension { get; }

    public RegisterVectorGroupMemberDefinition(IUnresolvedVectorGroupMemberType vector, int dimension, RegisterVectorGroupMemberLocations locations) : base(locations)
    {
        Vector = vector;

        Dimension = dimension;
    }
}
