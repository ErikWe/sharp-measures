namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;

public interface IVector : IQuantity
{
    public abstract int Dimension { get; }

    public abstract IRawScalarType? Scalar { get; }

    new public abstract IRawVectorType Difference { get; }
}
