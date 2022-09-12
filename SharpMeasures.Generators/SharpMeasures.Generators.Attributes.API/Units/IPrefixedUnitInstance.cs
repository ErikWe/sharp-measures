namespace SharpMeasures.Generators.Units;

public interface IPrefixedUnitInstance : IModifiedUnitInstance
{
    public abstract MetricPrefixName? MetricPrefix { get; }
    public abstract BinaryPrefixName? BinaryPrefix { get; }

    new public abstract IPrefixedUnitInstanceLocations Locations { get; }
}

public interface IPrefixedUnitInstanceLocations : IModifiedUnitInstanceLocations
{
    public abstract MinimalLocation? MetricPrefix { get; }
    public abstract MinimalLocation? BinaryPrefix { get; }

    public abstract bool ExplicitlySetMetricPrefix { get; }
    public abstract bool ExplicitlySetBinaryPrefix { get; }
}
