namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupMember : IQuantity
{
    public abstract NamedType VectorGroup { get; }

    public abstract int Dimension { get; }

    public abstract bool InheritOperations { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }

    public abstract bool InheritOperationsFromMembers { get; }
    public abstract bool InheritProcessesFromMembers { get; }
    public abstract bool InheritConstantsFromMembers { get; }
    public abstract bool InheritConversionsFromMembers { get; }
    public abstract bool InheritUnitsFromMembers { get; }

    new public abstract IVectorGroupMemberLocations Locations { get; }
}

public interface IVectorGroupMemberLocations : IQuantityLocations
{
    public abstract MinimalLocation? VectorGroup { get; }

    public abstract MinimalLocation? Dimension { get; }

    public abstract MinimalLocation? InheritOperations { get; }
    public abstract MinimalLocation? InheritConversions { get; }
    public abstract MinimalLocation? InheritUnits { get; }

    public abstract MinimalLocation? InheritOperationsFromMembers { get; }
    public abstract MinimalLocation? InheritProcessesFromMembers { get; }
    public abstract MinimalLocation? InheritConstantsFromMembers { get; }
    public abstract MinimalLocation? InheritConversionsFromMembers { get; }
    public abstract MinimalLocation? InheritUnitsFromMembers { get; }

    public abstract bool ExplicitlySetVectorGroup { get; }

    public abstract bool ExplicitlySetDimension { get; }

    public abstract bool ExplicitlySetInheritOperations { get; }
    public abstract bool ExplicitlySetInheritConversions { get; }
    public abstract bool ExplicitlySetInheritUnits { get; }

    public abstract bool ExplicitlySetInheritOperationsFromMembers { get; }
    public abstract bool ExplicitlySetInheritProcessesFromMembers { get; }
    public abstract bool ExplicitlySetInheritConstantsFromMembers { get; }
    public abstract bool ExplicitlySetInheritConversionsFromMembers { get; }
    public abstract bool ExplicitlySetInheritUnitsFromMembers { get; }
}
