namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitParsingData : AUnitParsingData
{
    internal static FixedUnitParsingData Empty { get; } = new();

    private FixedUnitParsingData() { }
}