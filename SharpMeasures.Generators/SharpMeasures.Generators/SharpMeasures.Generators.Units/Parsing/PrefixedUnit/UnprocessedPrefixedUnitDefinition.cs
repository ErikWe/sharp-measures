namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

internal record class UnprocessedPrefixedUnitDefinition : AUnprocessedDependantUnitDefinition<UnprocessedPrefixedUnitDefinition, PrefixedUnitLocations>
{
    public static UnprocessedPrefixedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    protected override UnprocessedPrefixedUnitDefinition Definition => this;

    private UnprocessedPrefixedUnitDefinition() : base(PrefixedUnitLocations.Empty) { }
}
