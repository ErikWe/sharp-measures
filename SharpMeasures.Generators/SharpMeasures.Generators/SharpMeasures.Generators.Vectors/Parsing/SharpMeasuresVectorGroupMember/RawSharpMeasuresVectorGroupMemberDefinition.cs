namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorGroupMemberDefinition : AUnprocessedAttributeDefinition<RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations>
{
    public static RawSharpMeasuresVectorGroupMemberDefinition Empty { get; } = new(SharpMeasuresVectorGroupMemberLocations.Empty);

    public NamedType? VectorGroup { get; init; }

    public int Dimension { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorGroupMemberDefinition Definition => this;

    private RawSharpMeasuresVectorGroupMemberDefinition(SharpMeasuresVectorGroupMemberLocations locations) : base(locations) { }
}
