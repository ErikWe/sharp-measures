namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Scalars;

public interface IUnresolvedScalar
{
    public abstract IScalar UnresolvedTarget { get; }

    public abstract NamedType Unit { get; }
    public abstract bool UseUnitBias { get; }

    public abstract NamedType? Vector { get; }

    public abstract bool ImplementSum { get; }
    public abstract bool ImplementDifference { get; }
    public abstract NamedType Difference { get; }

    public abstract string? DefaultUnitName { get; }
    public abstract string? DefaultUnitSymbol { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }

    public abstract bool GenerateDocumentation { get; }
}
