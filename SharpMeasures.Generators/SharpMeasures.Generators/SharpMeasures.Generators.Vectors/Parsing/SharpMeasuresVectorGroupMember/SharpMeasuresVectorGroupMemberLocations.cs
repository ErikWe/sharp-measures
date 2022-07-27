namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresVectorGroupMemberLocations : AAttributeLocations<SharpMeasuresVectorGroupMemberLocations>
{
    public static SharpMeasuresVectorGroupMemberLocations Empty => new();

    public MinimalLocation? VectorGroup { get; init; }
    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetVectorGroup => VectorGroup is not null;
    public bool ExplicitlySetDimension => Dimension is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresVectorGroupMemberLocations Locations => this;

    private SharpMeasuresVectorGroupMemberLocations() { }
}
