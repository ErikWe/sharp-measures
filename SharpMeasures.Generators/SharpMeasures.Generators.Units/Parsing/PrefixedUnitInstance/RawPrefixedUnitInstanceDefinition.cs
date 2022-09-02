namespace SharpMeasures.Generators.Units.Parsing.PrefixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.Utility;

internal record class RawPrefixedUnitInstanceDefinition : ARawModifiedUnitDefinition<RawPrefixedUnitInstanceDefinition, PrefixedUnitInstanceLocations>
{
    public static RawPrefixedUnitInstanceDefinition Empty { get; } = new();

    public MetricPrefixName? MetricPrefix { get; init; }
    public BinaryPrefixName? BinaryPrefix { get; init; }

    protected override RawPrefixedUnitInstanceDefinition Definition => this;

    private RawPrefixedUnitInstanceDefinition() : base(PrefixedUnitInstanceLocations.Empty) { }
}
