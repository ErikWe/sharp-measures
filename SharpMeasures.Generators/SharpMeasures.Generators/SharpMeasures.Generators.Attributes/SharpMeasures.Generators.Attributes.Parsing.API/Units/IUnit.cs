namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators.Unresolved.Scalars;

public interface IUnit
{
    public abstract IUnresolvedScalarType Quantity { get; }

    public abstract bool BiasTerm { get; }

    public abstract bool GenerateDocumentation { get; }
}
