namespace SharpMeasures.Generators.Quantities;

public interface IQuantityBase : IQuantity
{
    public abstract NamedType Unit { get; }

    new public abstract bool ImplementSum { get; }
    new public abstract bool ImplementDifference { get; }

    new public abstract IQuantityBaseLocations Locations { get; }
}

public interface IQuantityBaseLocations : IQuantityLocations
{
    public abstract MinimalLocation? Unit { get; }

    public abstract bool ExplicitlySetUnit { get; }
}
