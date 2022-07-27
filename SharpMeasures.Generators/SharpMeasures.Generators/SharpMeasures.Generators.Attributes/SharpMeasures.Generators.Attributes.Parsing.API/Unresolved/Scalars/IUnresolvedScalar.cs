namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalar : IUnresolvedQuantity
{
    public abstract NamedType? VectorGroup { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }
}
