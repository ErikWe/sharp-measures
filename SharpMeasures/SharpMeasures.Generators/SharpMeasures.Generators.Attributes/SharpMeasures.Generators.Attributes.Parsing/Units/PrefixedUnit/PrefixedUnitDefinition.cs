namespace SharpMeasures.Generators.Attributes.Parsing.Units;

using SharpMeasures.Generators.Units;

public record class PrefixedUnitDefinition : ADependantUnitDefinition<PrefixedUnitParsingData, PrefixedUnitLocations>
{
    internal static PrefixedUnitDefinition Empty { get; } = new();

    public string From => DependantOn;
    public MetricPrefixName MetricPrefixName { get; init; }
    public BinaryPrefixName BinaryPrefixName { get; init; }

    private PrefixedUnitDefinition() : base(PrefixedUnitParsingData.Empty) { }
}