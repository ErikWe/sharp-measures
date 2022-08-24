namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroupMember : IQuantity
{
    public abstract NamedType VectorGroup { get; }

    public abstract int Dimension { get; }

    public abstract bool InheritDerivations { get; }
    public abstract bool InheritConversions { get; }
    public abstract bool InheritUnits { get; }

    public abstract bool InheritDerivationsFromMembers { get; }
    public abstract bool InheritConstantsFromMembers { get; }
    public abstract bool InheritConversionsFromMembers { get; }
    public abstract bool InheritUnitsFromMembers { get; }
}
