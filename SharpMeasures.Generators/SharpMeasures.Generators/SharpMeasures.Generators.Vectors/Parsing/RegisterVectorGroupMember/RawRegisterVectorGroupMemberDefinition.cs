namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawRegisterVectorGroupMemberDefinition : ARawAttributeDefinition<RawRegisterVectorGroupMemberDefinition, RegisterVectorGroupMemberLocations>
{
    public static RawRegisterVectorGroupMemberDefinition Empty { get; } = new(RegisterVectorGroupMemberLocations.Empty);

    public NamedType? Vector { get; init; }

    public int Dimension { get; init; }

    protected override RawRegisterVectorGroupMemberDefinition Definition => this;

    private RawRegisterVectorGroupMemberDefinition(RegisterVectorGroupMemberLocations locations) : base(locations) { }
}
