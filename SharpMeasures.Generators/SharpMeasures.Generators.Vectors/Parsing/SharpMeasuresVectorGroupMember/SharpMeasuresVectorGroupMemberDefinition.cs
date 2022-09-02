namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors;

internal record class SharpMeasuresVectorGroupMemberDefinition : AAttributeDefinition<SharpMeasuresVectorGroupMemberLocations>, IVectorGroupMember
{
    public NamedType VectorGroup { get; }

    public bool InheritDerivations { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public bool InheritDerivationsFromMembers { get; }
    public bool InheritConstantsFromMembers { get; }
    public bool InheritConversionsFromMembers { get; }
    public bool InheritUnitsFromMembers { get; }

    public int Dimension { get; }

    public bool? GenerateDocumentation { get; }

    bool? IQuantity.ImplementSum => null;
    bool? IQuantity.ImplementDifference => null;
    NamedType? IQuantity.Difference => null;

    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceName => null;
    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceSymbol => null;

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IVectorGroupMemberLocations IVectorGroupMember.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SharpMeasuresVectorGroupMemberDefinition(NamedType vectorGroup, bool inheritDerivations, bool inheritConversions, bool inheritUnits, bool inheritDerivationsFromMembers, bool inheritConstantsFromMembers,
        bool inheritConversionsFromMembers, bool inheritUnitsFromMembers, int dimension, bool? generateDocumentation, SharpMeasuresVectorGroupMemberLocations locations)
        : base(locations)
    {
        VectorGroup = vectorGroup;

        InheritDerivations = inheritDerivations;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;

        InheritDerivationsFromMembers = inheritDerivationsFromMembers;
        InheritConstantsFromMembers = inheritConstantsFromMembers;
        InheritConversionsFromMembers = inheritConversionsFromMembers;
        InheritUnitsFromMembers = inheritUnitsFromMembers;

        Dimension = dimension;

        GenerateDocumentation = generateDocumentation;
    }
}
