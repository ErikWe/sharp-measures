namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Generators.Quantities;

public interface IScalarBase : IScalar, IQuantityBase
{
    public abstract bool UseUnitBias { get; }

    new public abstract IScalarBaseLocations Locations { get; }
}

public interface IScalarBaseLocations : IScalarLocations, IQuantityBaseLocations
{
    public abstract MinimalLocation? UseUnitBias { get; }

    public abstract bool ExplicitlySetUseUnitBias { get; }
}
