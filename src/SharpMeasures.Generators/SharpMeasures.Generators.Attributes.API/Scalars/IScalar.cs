namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalar : IQuantity
{
    public abstract NamedType? Vector { get; }

    new public abstract IScalarLocations Locations { get; }
}

public interface IScalarLocations : IQuantityLocations
{
    public abstract MinimalLocation? Vector { get; }

    public abstract bool ExplicitlySetVector { get; }
}
