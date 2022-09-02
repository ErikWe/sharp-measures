namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVector : IQuantity
{
    public abstract NamedType? Scalar { get; }

    new public abstract IVectorLocations Locations { get; }
}

public interface IVectorLocations : IQuantityLocations
{
    public abstract MinimalLocation? Scalar { get; }

    public abstract bool ExplicitlySetScalar { get; }
}
