namespace SharpMeasures.Generators.Units;

public interface IUnit : ISharpMeasuresObject
{
    public abstract NamedType Quantity { get; }

    public abstract bool BiasTerm { get; }
}
