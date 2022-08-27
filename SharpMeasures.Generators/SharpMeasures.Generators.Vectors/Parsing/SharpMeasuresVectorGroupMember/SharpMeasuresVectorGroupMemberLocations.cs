namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresVectorGroupMemberLocations : AAttributeLocations<SharpMeasuresVectorGroupMemberLocations>
{
    public static SharpMeasuresVectorGroupMemberLocations Empty => new();

    public MinimalLocation? VectorGroup { get; init; }

    public MinimalLocation? InheritDerivations { get; init; }
    public MinimalLocation? InheritConversions { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? InheritDerivationsFromMembers { get; init; }
    public MinimalLocation? InheritConstantsFromMembers { get; init; }
    public MinimalLocation? InheritConversionsFromMembers { get; init; }
    public MinimalLocation? InheritUnitsFromMembers { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetVectorGroup => VectorGroup is not null;
    public bool ExplicitlySetInheritDerivations => InheritDerivations is not null;
    public bool ExplicitlySetInheritConversions => InheritConversions is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;
    public bool ExplicitlySetInheritDerivationsFromMembers => InheritDerivationsFromMembers is not null;
    public bool ExplicitlySetInheritConstantsFromMembers => InheritConstantsFromMembers is not null;
    public bool ExplicitlySetInheritConversionsFromMembers => InheritConversionsFromMembers is not null;
    public bool ExplicitlySetInheritUnitsFromMembers => InheritUnitsFromMembers is not null;
    public bool ExplicitlySetDimension => Dimension is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    protected override SharpMeasuresVectorGroupMemberLocations Locations => this;

    private SharpMeasuresVectorGroupMemberLocations() { }
}
