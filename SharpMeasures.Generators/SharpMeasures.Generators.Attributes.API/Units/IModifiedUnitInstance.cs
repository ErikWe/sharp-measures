namespace SharpMeasures.Generators.Units;

using SharpMeasures.Generators;

public interface IModifiedUnitInstance : IUnitInstance
{
    public abstract string OriginalUnitInstance { get; }

    new public abstract IModifiedUnitInstanceLocations Locations { get; }
}

public interface IModifiedUnitInstanceLocations : IUnitInstanceLocations
{
    public abstract MinimalLocation? OriginalUnitInstance { get; }

    public abstract bool ExplicitlySetOriginalUnitInstance { get; }
}
