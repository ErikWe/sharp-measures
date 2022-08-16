namespace SharpMeasures.Generators.Raw.Scalars;

using SharpMeasures.Generators.Raw.Quantities;

public interface IRawScalarConstant : IRawQuantityConstant
{
    public abstract double Value { get; }
}
