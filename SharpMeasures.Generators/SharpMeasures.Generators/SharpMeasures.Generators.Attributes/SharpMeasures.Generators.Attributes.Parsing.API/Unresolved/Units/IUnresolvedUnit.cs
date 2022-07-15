namespace SharpMeasures.Generators.Unresolved.Units;

public interface IUnresolvedUnit : ISharpMeasuresObject
{
    public abstract NamedType Quantity { get; }

    public abstract bool BiasTerm { get; }
}
