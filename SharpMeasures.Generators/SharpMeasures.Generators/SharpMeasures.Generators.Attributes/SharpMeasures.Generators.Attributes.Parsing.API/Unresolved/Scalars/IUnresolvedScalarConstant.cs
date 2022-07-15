namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Unresolved.Quantities;

public interface IUnresolvedScalarConstant : IUnresolvedQuantityConstant
{
    public abstract double Value { get; }
}
