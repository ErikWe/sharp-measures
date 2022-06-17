namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitAliasParsingData : AUnitParsingData
{
    public static UnitAliasParsingData Empty { get; } = new();

    private UnitAliasParsingData() { }
}
