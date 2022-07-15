namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedSpecializedScalarType : IUnresolvedScalarType, IUnresolvedSpecializedQuantityType
{
    new public abstract IUnresolvedSpecializedScalar Definition { get; }
}
