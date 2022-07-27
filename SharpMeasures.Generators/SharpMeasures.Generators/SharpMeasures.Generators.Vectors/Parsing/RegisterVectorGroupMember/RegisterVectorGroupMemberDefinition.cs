namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class RegisterVectorGroupMemberDefinition : AAttributeDefinition<RegisterVectorGroupMemberLocations>, IRegisteredVectorGroupMember
{
    public IUnresolvedIndividualVectorType Vector { get; }

    public int Dimension { get; }

    public RegisterVectorGroupMemberDefinition(IUnresolvedIndividualVectorType vector, int dimension, RegisterVectorGroupMemberLocations locations) : base(locations)
    {
        Vector = vector;

        Dimension = dimension;
    }
}
