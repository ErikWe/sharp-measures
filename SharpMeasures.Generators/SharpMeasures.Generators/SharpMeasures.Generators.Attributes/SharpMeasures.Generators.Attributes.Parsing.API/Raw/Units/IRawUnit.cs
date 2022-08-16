namespace SharpMeasures.Generators.Raw.Units;

public interface IRawUnit : ISharpMeasuresObject
{
    public abstract NamedType Quantity { get; }

    public abstract bool BiasTerm { get; }
}
