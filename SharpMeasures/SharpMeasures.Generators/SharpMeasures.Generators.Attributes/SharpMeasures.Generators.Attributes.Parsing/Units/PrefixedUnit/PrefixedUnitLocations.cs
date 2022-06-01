namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class PrefixedUnitLocations : ADependantUnitLocations
{
    internal static PrefixedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? MetricPrefixName { get; init; }
    public MinimalLocation? BinaryPrefixName { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetMetricPrefixName => MetricPrefixName is not null;
    public bool ExplicitlySetBinaryPrefixName => BinaryPrefixName is not null;

    private PrefixedUnitLocations() { }
}