namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawOffsetUnit : ARawDependantUnitDefinition<OffsetUnitParsingData, OffsetUnitLocations>
{
    internal static RawOffsetUnit Empty { get; } = new();

    public string? From => DependantOn;
    public double Offset { get; init; }

    private RawOffsetUnit() : base(OffsetUnitLocations.Empty, OffsetUnitParsingData.Empty) { }
}
