namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;

internal sealed record class SharpMeasuresVectorGroupMemberLocations : AAttributeLocations<SharpMeasuresVectorGroupMemberLocations>, IVectorGroupMemberLocations
{
    public static SharpMeasuresVectorGroupMemberLocations Empty => new();

    public MinimalLocation? VectorGroup { get; init; }

    public MinimalLocation? InheritOperations { get; init; }
    public MinimalLocation? InheritConversions { get; init; }
    public MinimalLocation? InheritUnits { get; init; }

    public MinimalLocation? InheritOperationsFromMembers { get; init; }
    public MinimalLocation? InheritProcessesFromMembers { get; init; }
    public MinimalLocation? InheritConstantsFromMembers { get; init; }
    public MinimalLocation? InheritConversionsFromMembers { get; init; }
    public MinimalLocation? InheritUnitsFromMembers { get; init; }

    public MinimalLocation? Dimension { get; init; }

    public MinimalLocation? GenerateDocumentation { get; init; }

    MinimalLocation? IQuantityLocations.ImplementSum => null;
    MinimalLocation? IQuantityLocations.ImplementDifference => null;
    MinimalLocation? IQuantityLocations.Difference => null;

    MinimalLocation? IDefaultUnitInstanceLocations.DefaultUnitInstanceName => null;
    MinimalLocation? IDefaultUnitInstanceLocations.DefaultUnitInstanceSymbol => null;

    public bool ExplicitlySetVectorGroup => VectorGroup is not null;

    public bool ExplicitlySetInheritOperations => InheritOperations is not null;
    public bool ExplicitlySetInheritConversions => InheritConversions is not null;
    public bool ExplicitlySetInheritUnits => InheritUnits is not null;

    public bool ExplicitlySetInheritOperationsFromMembers => InheritOperationsFromMembers is not null;
    public bool ExplicitlySetInheritProcessesFromMembers => InheritProcessesFromMembers is not null;
    public bool ExplicitlySetInheritConstantsFromMembers => InheritConstantsFromMembers is not null;
    public bool ExplicitlySetInheritConversionsFromMembers => InheritConversionsFromMembers is not null;
    public bool ExplicitlySetInheritUnitsFromMembers => InheritUnitsFromMembers is not null;

    public bool ExplicitlySetDimension => Dimension is not null;

    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    bool IQuantityLocations.ExplicitlySetImplementSum => false;
    bool IQuantityLocations.ExplicitlySetImplementDifference => false;
    bool IQuantityLocations.ExplicitlySetDifference => false;

    bool IDefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceName => false;
    bool IDefaultUnitInstanceLocations.ExplicitlySetDefaultUnitInstanceSymbol => false;

    protected override SharpMeasuresVectorGroupMemberLocations Locations => this;

    private SharpMeasuresVectorGroupMemberLocations() { }
}
