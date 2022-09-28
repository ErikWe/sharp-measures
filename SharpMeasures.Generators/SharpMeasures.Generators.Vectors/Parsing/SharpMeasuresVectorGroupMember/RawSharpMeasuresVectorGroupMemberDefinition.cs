namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class RawSharpMeasuresVectorGroupMemberDefinition : ARawAttributeDefinition<RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations>, IDefaultUnitInstanceDefinition
{
    public static RawSharpMeasuresVectorGroupMemberDefinition FromSymbolic(SymbolicSharpMeasuresVectorGroupMemberDefinition symbolicDefinition) => new RawSharpMeasuresVectorGroupMemberDefinition(symbolicDefinition.Locations) with
    {
        VectorGroup = symbolicDefinition.VectorGroup?.AsNamedType(),
        InheritDerivations = symbolicDefinition.InheritDerivations,
        InheritConversions = symbolicDefinition.InheritConversions,
        InheritUnits = symbolicDefinition.InheritUnits,
        InheritDerivationsFromMembers = symbolicDefinition.InheritDerivationsFromMembers,
        InheritProcessesFromMembers = symbolicDefinition.InheritProcessesFromMembers,
        InheritConstantsFromMembers = symbolicDefinition.InheritConstantsFromMembers,
        InheritConversionsFromMembers = symbolicDefinition.InheritConversionsFromMembers,
        InheritUnitsFromMembers = symbolicDefinition.InheritUnitsFromMembers,
        Dimension = symbolicDefinition.Dimension,
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    public NamedType? VectorGroup { get; init; }

    public bool InheritDerivations { get; init; } = true;
    public bool InheritConversions { get; init; } = true;
    public bool InheritUnits { get; init; } = true;

    public bool? InheritDerivationsFromMembers { get; init; }
    public bool InheritProcessesFromMembers { get; init; }
    public bool InheritConstantsFromMembers { get; init; } = true;
    public bool? InheritConversionsFromMembers { get; init; }
    public bool? InheritUnitsFromMembers { get; init; }

    public int? Dimension { get; init; }

    public bool? GenerateDocumentation { get; init; }

    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceName => null;
    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceSymbol => null;

    protected override RawSharpMeasuresVectorGroupMemberDefinition Definition => this;

    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    private RawSharpMeasuresVectorGroupMemberDefinition(SharpMeasuresVectorGroupMemberLocations locations) : base(locations) { }
}
