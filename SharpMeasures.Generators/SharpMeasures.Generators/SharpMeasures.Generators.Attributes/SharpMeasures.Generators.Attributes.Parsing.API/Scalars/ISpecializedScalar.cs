namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Unresolved.Scalars;

public interface ISpecializedScalar : IScalar, ISpecializedQuantity
{
    public abstract IUnresolvedScalarType OriginalScalar { get; }

    public abstract bool InheritConvertibleScalars { get; }
    public abstract bool InheritBases { get; }
}
