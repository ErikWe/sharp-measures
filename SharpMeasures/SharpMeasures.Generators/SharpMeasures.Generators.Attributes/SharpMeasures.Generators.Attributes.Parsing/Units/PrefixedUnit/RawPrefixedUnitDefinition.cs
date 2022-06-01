namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public record class RawPrefixedUnitDefinition : ARawDependantUnitDefinition<PrefixedUnitParsingData, PrefixedUnitLocations>
{
    internal static RawPrefixedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    private RawPrefixedUnitDefinition() : base(PrefixedUnitLocations.Empty, PrefixedUnitParsingData.Empty) { }
}