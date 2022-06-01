namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class PrefixedUnitParsingData : AUnitParsingData
{
    internal static PrefixedUnitParsingData Empty { get; } = new();

    private PrefixedUnitParsingData() { }
}