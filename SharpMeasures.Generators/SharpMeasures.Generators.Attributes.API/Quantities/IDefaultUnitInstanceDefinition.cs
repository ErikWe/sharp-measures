namespace SharpMeasures.Generators.Quantities;

public interface IDefaultUnitInstanceDefinition
{
    public abstract string? DefaultUnitInstanceName { get; }
    public abstract string? DefaultUnitInstanceSymbol { get; }

    public abstract IDefaultUnitInstanceLocations DefaultUnitInstanceLocations { get; }
}

public interface IDefaultUnitInstanceLocations
{
    public abstract MinimalLocation? DefaultUnitInstanceName { get; }
    public abstract MinimalLocation? DefaultUnitInstanceSymbol { get; }

    public abstract bool ExplicitlySetDefaultUnitInstanceName { get; }
    public abstract bool ExplicitlySetDefaultUnitInstanceSymbol { get; }
}
