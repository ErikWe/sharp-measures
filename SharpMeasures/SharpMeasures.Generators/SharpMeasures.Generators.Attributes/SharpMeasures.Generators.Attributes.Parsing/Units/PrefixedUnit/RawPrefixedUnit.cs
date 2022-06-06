namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public record class RawPrefixedUnit : ARawDependantUnitDefinition<PrefixedUnitParsingData, PrefixedUnitLocations>
{
    internal static RawPrefixedUnit Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    private RawPrefixedUnit() : base(PrefixedUnitLocations.Empty, PrefixedUnitParsingData.Empty) { }
}
