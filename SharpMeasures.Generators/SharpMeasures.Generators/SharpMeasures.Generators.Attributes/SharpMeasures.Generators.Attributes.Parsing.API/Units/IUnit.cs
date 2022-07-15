namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Unresolved.Scalars;

public interface IUnit : ISharpMeasuresObject
{
    public abstract IUnresolvedScalarType Quantity { get; }

    public abstract bool BiasTerm { get; }
}
