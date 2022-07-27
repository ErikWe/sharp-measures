namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RegisterVectorGroupMemberLocations : AAttributeLocations<RegisterVectorGroupMemberLocations>
{
    public static RegisterVectorGroupMemberLocations Empty { get; } = new();

    public MinimalLocation? Vector { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public bool ExplicitlySetVector => Vector is not null;
    public bool ExplicitlySetDimension => Dimension is not null;

    protected override RegisterVectorGroupMemberLocations Locations => this;

    private RegisterVectorGroupMemberLocations() { }
}
