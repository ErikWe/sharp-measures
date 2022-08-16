namespace SharpMeasures.Generators.Vectors.Groups;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Vectors.Groups;

public interface IVectorGroup : IQuantity
{
    public abstract IRawScalarType? Scalar { get; }

    new public abstract IRawVectorGroupType Difference { get; }
}
