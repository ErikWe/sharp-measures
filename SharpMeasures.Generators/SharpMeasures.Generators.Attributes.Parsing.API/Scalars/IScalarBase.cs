namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarBase : IScalar, IQuantityBase
{
    public abstract bool UseUnitBias { get; }
}
