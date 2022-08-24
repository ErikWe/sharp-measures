namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVector : IQuantity
{
    public abstract NamedType? Scalar { get; }
}
