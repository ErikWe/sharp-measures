namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class PrefixedUnitInstanceLocations : AModifiedUnitInstanceLocations<PrefixedUnitInstanceLocations>, IPrefixedUnitInstanceLocations
{
    public static PrefixedUnitInstanceLocations Empty { get; } = new();

    public MinimalLocation? MetricPrefix { get; init; }
    public MinimalLocation? BinaryPrefix { get; init; }

    public bool ExplicitlySetMetricPrefix => MetricPrefix is not null;
    public bool ExplicitlySetBinaryPrefix => BinaryPrefix is not null;

    protected override PrefixedUnitInstanceLocations Locations => this;

    private PrefixedUnitInstanceLocations() { }
}
