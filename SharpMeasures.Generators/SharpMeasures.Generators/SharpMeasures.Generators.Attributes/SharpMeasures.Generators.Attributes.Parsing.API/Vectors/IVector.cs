namespace SharpMeasures.Generators.Vectors;

public interface IVector
{
    public int Dimension { get; }

    public abstract NamedType? Scalar { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }

    public abstract NamedType? Difference { get; }

    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract bool GenerateDocumentation { get; }
}
