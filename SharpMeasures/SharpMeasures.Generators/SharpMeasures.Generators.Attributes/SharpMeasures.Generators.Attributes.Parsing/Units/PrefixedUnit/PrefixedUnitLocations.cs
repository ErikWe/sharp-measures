namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class PrefixedUnitLocations : ADependantUnitLocations
{
    internal static PrefixedUnitLocations Empty { get; } = new();

    public MinimalLocation From => DependantOn;
    public MinimalLocation MetricPrefixName { get; init; }
    public MinimalLocation BinaryPrefixName { get; init; }

    private PrefixedUnitLocations() { }
}