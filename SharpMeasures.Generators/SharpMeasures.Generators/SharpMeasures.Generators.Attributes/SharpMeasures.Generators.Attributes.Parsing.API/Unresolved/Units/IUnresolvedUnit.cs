namespace SharpMeasures.Generators.Unresolved.Units;

public interface IUnresolvedUnit
{
    public abstract NamedType Quantity { get; }

    public abstract bool BiasTerm { get; }

    public abstract bool GenerateDocumentation { get; }
}
