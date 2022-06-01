namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawOffsetUnitDefinition : ARawDependantUnitDefinition<OffsetUnitParsingData, OffsetUnitLocations>
{
    internal static RawOffsetUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Offset { get; init; }

    private RawOffsetUnitDefinition() : base(OffsetUnitLocations.Empty, OffsetUnitParsingData.Empty) { }
}