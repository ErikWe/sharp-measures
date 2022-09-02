namespace SharpMeasures.Generators.Vectors;

using SharpMeasures.Generators.Quantities;

public interface IVectorBase : IVector, IQuantityBase
{
    public abstract int Dimension { get; }

    new public abstract IVectorBaseLocations Locations { get; }
}

public interface IVectorBaseLocations : IVectorLocations, IQuantityBaseLocations
{
    public abstract MinimalLocation? Dimension { get; }

    public abstract bool ExplicitlySetDimension { get; }
}
