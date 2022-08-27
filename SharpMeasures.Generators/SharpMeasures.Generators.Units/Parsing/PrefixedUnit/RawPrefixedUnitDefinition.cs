namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

internal record class RawPrefixedUnitDefinition : ARawDependantUnitDefinition<RawPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public static RawPrefixedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefix { get; init; }
    public BinaryPrefixName BinaryPrefix { get; init; }

    protected override RawPrefixedUnitDefinition Definition => this;

    private RawPrefixedUnitDefinition() : base(PrefixedUnitLocations.Empty) { }
}
