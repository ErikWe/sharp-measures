namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Vectors;

public interface IVector : IQuantity
{
    public int Dimension { get; }

    public abstract IUnresolvedScalarType? Scalar { get; }

    new public abstract IUnresolvedVectorType? Difference { get; }
}
