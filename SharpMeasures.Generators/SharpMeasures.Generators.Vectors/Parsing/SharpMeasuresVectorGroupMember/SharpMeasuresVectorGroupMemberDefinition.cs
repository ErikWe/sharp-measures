namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SharpMeasuresVectorGroupMemberDefinition : AAttributeDefinition<SharpMeasuresVectorGroupMemberLocations>, IVectorGroupMember
{
    public NamedType VectorGroup { get; }

    public bool InheritOperations { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public bool InheritOperationsFromMembers { get; }
    public bool InheritProcessesFromMembers { get; }
    public bool InheritConstantsFromMembers { get; }
    public bool InheritConversionsFromMembers { get; }
    public bool InheritUnitsFromMembers { get; }

    public int Dimension { get; }

    bool? IQuantity.ImplementSum => null;
    bool? IQuantity.ImplementDifference => null;
    NamedType? IQuantity.Difference => null;

    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceName => null;
    string? IDefaultUnitInstanceDefinition.DefaultUnitInstanceSymbol => null;

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IQuantityLocations IQuantity.Locations => Locations;
    IVectorGroupMemberLocations IVectorGroupMember.Locations => Locations;
    IDefaultUnitInstanceLocations IDefaultUnitInstanceDefinition.DefaultUnitInstanceLocations => Locations;

    public SharpMeasuresVectorGroupMemberDefinition(NamedType vectorGroup, bool inheritOperations, bool inheritConversions, bool inheritUnits, bool inheritOperationsFromMembers, bool inheritProcessesFromMembers, bool inheritConstantsFromMembers,
        bool inheritConversionsFromMembers, bool inheritUnitsFromMembers, int dimension, SharpMeasuresVectorGroupMemberLocations locations) : base(locations)
    {
        VectorGroup = vectorGroup;

        InheritOperations = inheritOperations;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;

        InheritOperationsFromMembers = inheritOperationsFromMembers;
        InheritProcessesFromMembers = inheritProcessesFromMembers;
        InheritConstantsFromMembers = inheritConstantsFromMembers;
        InheritConversionsFromMembers = inheritConversionsFromMembers;
        InheritUnitsFromMembers = inheritUnitsFromMembers;

        Dimension = dimension;
    }
}
