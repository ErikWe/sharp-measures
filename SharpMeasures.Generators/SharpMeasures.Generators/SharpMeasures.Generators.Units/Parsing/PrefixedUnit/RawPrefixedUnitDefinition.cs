namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

internal record class RawPrefixedUnitDefinition : AUnprocessedDependantUnitDefinition<RawPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public static RawPrefixedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    protected override RawPrefixedUnitDefinition Definition => this;

    private RawPrefixedUnitDefinition() : base(PrefixedUnitLocations.Empty) { }
}
