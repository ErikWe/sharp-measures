namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorBase : IVector, IQuantityBase
{
    public abstract int Dimension { get; }
}
