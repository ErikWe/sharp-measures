namespace SharpMeasures.Generators.Units;

public interface IUnit : ISharpMeasuresObject
{
    public abstract NamedType Quantity { get; }

    public abstract bool BiasTerm { get; }

    new public abstract IUnitLocations Locations { get; }
}

public interface IUnitLocations : ISharpMeasuresObjectLocations
{
    public abstract MinimalLocation? Quantity { get; }

    public abstract MinimalLocation? BiasTerm { get; }

    public abstract bool ExplicitlySetQuantity { get; }

    public abstract bool ExplicitlySetBiasTerm { get; }
}
