namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;

public interface IBaseScalar : IScalar
{
    public abstract IUnresolvedUnitType Unit { get; }
    public abstract bool UseUnitBias { get; }

    new public abstract IUnresolvedScalarType Difference { get; }
}
