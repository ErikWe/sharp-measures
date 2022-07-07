namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitLocations : ADependantUnitLocations<PrefixedUnitLocations>
{
    public static PrefixedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? MetricPrefixName { get; init; }
    public MinimalLocation? BinaryPrefixName { get; init; }

    public bool ExplicitlySetFrom => ExplicitlySetDependantOn;
    public bool ExplicitlySetMetricPrefixName => MetricPrefixName is not null;
    public bool ExplicitlySetBinaryPrefixName => BinaryPrefixName is not null;

    protected override PrefixedUnitLocations Locations => this;

    private PrefixedUnitLocations() { }
}
