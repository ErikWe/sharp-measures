namespace SharpMeasures.Generators.Raw.Vectors;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawVectorBase : IRawVector, IRawQuantityBase
{
    public abstract int Dimension { get; }
}
