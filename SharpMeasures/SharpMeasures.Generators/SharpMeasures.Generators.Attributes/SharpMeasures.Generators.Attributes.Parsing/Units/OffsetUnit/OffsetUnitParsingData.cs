namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnitParsingData : AUnitParsingData
{
    internal static OffsetUnitParsingData Empty { get; } = new();

    private OffsetUnitParsingData() { }
}