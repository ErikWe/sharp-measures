namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresVectorGroupMemberDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations>
{
    public static RawSharpMeasuresVectorGroupMemberDefinition Empty { get; } = new(SharpMeasuresVectorGroupMemberLocations.Empty);

    public NamedType? VectorGroup { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public bool? InheritDerivationsFromMembers { get; init; }
    public bool InheritConstantsFromMembers { get; init; } = true;
    public bool? InheritConversionsFromMembers { get; init; }
    public bool? InheritUnitsFromMembers { get; init; }

    public int? Dimension { get; init; }

    public bool? GenerateDocumentation { get; init; }

    protected override RawSharpMeasuresVectorGroupMemberDefinition Definition => this;

    private RawSharpMeasuresVectorGroupMemberDefinition(SharpMeasuresVectorGroupMemberLocations locations) : base(locations) { }
}
