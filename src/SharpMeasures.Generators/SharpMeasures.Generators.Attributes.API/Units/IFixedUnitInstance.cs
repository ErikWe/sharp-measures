namespace SharpMeasures.Generators.Units;

public interface IFixedUnitInstance : IUnitInstance
{
    new public abstract IFixedUnitInstanceLocations Locations { get; }
}

public interface IFixedUnitInstanceLocations : IUnitInstanceLocations { }
