namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarConstant : IQuantityConstant
{
    public abstract double Value { get; }
}
