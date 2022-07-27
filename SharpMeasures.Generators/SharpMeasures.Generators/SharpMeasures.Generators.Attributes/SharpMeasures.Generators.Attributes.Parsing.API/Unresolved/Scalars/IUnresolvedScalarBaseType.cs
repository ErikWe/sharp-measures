namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalarBaseType : IUnresolvedScalarType, IUnresolvedQuantityBaseType
{
    new public abstract IUnresolvedScalarBase Definition { get; }
}
