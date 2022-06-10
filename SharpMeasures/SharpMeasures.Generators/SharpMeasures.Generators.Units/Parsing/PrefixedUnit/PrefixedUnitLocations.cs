namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitLocations : ADependantUnitLocations
{
    public static PrefixedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? MetricPrefixName { get; init; }
    public MinimalLocation? BinaryPrefixName { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetMetricPrefixName => MetricPrefixName is not null;
    public bool ExplicitlySetBinaryPrefixName => BinaryPrefixName is not null;

    private PrefixedUnitLocations() { }
}
