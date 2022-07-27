namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Vectors;

public interface IVectorGroup : IQuantity
{
    public abstract IUnresolvedScalarType? Scalar { get; }

    new public abstract IUnresolvedVectorGroupType Difference { get; }
}
