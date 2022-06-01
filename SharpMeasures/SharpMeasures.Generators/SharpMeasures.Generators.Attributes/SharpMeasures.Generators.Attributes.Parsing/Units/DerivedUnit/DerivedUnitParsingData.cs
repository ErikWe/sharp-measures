namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class DerivedUnitParsingData : AUnitParsingData
{
    internal static DerivedUnitParsingData Empty { get; } = new();

    private DerivedUnitParsingData() { }
}
