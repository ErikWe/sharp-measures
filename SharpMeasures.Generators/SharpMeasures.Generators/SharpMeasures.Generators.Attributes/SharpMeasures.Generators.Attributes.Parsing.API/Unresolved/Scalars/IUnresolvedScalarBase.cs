namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalarBase : IUnresolvedScalar, IUnresolvedQuantityBase
{
    public abstract bool UseUnitBias { get; }
}
