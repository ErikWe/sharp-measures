namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

internal record class RawPrefixedUnitDefinition : ARawDependantUnitDefinition<PrefixedUnitParsingData, PrefixedUnitLocations>
{
    public static RawPrefixedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    private RawPrefixedUnitDefinition() : base(PrefixedUnitLocations.Empty, PrefixedUnitParsingData.Empty) { }
}
