namespace SharpMeasures.Generators.Units;
public interface IUnitInstanceAlias : IModifiedUnitInstance
{
    new public abstract IUnitInstanceAliasLocations Locations { get; }
}

public interface IUnitInstanceAliasLocations : IModifiedUnitInstanceLocations { }
