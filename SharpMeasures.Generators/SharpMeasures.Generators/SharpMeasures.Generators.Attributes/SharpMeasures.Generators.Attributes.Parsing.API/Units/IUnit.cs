namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Raw.Scalars;

public interface IUnit : ISharpMeasuresObject
{
    public abstract IRawScalarType Quantity { get; }

    public abstract bool BiasTerm { get; }
}
