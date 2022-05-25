namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnitParsingData : ADependantUnitParsingData<OffsetUnitLocations>
{
    internal static OffsetUnitParsingData Empty { get; } = new();

    private OffsetUnitParsingData() : base(OffsetUnitLocations.Empty) { }
}