namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedVectorGroup : IUnresolvedQuantity
{
    public abstract NamedType? Scalar { get; }
}
