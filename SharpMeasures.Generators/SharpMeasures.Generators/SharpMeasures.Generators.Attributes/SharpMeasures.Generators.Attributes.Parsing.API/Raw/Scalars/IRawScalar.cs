namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalar : IRawQuantity
{
    public abstract NamedType? Vector { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }
}
