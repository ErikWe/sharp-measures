namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalarBase : IRawScalar, IRawQuantityBase
{
    public abstract bool UseUnitBias { get; }
}
