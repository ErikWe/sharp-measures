namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnitParsingData : AUnitParsingData
{
    internal static ScaledUnitParsingData Empty { get; } = new();

    private ScaledUnitParsingData() { }
}