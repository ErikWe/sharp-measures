namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedSpecializedScalar : IUnresolvedScalar, IUnresolvedSpecializedQuantity
{
    public abstract NamedType OriginalScalar { get; }
    
    public abstract bool InheritConvertibleScalars { get; }
    public abstract bool InheritBases { get; }
}
