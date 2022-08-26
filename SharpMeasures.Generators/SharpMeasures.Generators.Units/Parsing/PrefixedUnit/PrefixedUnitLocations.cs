namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitLocations : ADependantUnitLocations<PrefixedUnitLocations>
{
    public static PrefixedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? MetricPrefix { get; init; }
    public MinimalLocation? BinaryPrefix { get; init; }

    public bool ExplicitlySetFrom => ExplicitlySetDependantOn;
    public bool ExplicitlySetMetricPrefix => MetricPrefix is not null;
    public bool ExplicitlySetBinaryPrefix => BinaryPrefix is not null;

    protected override PrefixedUnitLocations Locations => this;

    private PrefixedUnitLocations() { }
}
