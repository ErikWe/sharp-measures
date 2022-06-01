namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasParsingData : AUnitParsingData
{
    internal static UnitAliasParsingData Empty { get; } = new();

    private UnitAliasParsingData() { }
}