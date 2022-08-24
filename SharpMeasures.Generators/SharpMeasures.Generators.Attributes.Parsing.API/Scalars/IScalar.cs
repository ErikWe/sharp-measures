namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalar : IQuantity
{
    public abstract NamedType? Vector { get; }

    public abstract NamedType? Reciprocal { get; }
    public abstract NamedType? Square { get; }
    public abstract NamedType? Cube { get; }
    public abstract NamedType? SquareRoot { get; }
    public abstract NamedType? CubeRoot { get; }
}
