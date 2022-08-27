namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroup : IQuantity
{
    public abstract NamedType? Scalar { get; }
}
