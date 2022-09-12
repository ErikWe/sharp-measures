namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorGroup : IQuantity
{
    public abstract NamedType? Scalar { get; }

    new public abstract IVectorGroupLocations Locations { get; }
}

public interface IVectorGroupLocations : IQuantityLocations
{
    public abstract MinimalLocation? Scalar { get; }

    public abstract bool ExplicitlySetScalar { get; }
}
