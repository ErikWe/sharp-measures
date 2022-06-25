namespace SharpMeasures.Generators.Vectors;

public interface IRootVector : IVector
{
    public abstract NamedType UnitType { get; }
    public abstract NamedType? ScalarType { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }

    public abstract NamedType Difference { get; }

    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }
}
