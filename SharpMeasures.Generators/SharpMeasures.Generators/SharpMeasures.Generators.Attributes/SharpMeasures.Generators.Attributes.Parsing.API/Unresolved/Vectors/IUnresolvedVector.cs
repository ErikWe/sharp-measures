namespace SharpMeasures.Generators.Unresolved.Vectors;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedVector : IUnresolvedQuantity
{
    public int Dimension { get; }

    public abstract NamedType? Scalar { get; }
}
