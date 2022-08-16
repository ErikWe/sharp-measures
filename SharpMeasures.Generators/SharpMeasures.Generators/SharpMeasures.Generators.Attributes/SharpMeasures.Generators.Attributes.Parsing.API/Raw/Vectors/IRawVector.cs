namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVector : IRawQuantity
{
    public abstract NamedType? Scalar { get; }
}
