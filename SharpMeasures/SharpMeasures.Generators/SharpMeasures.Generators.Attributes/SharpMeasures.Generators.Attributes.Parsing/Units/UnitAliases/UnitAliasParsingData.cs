namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasParsingData : ADependantUnitParsingData<UnitAliasLocations>
{
    internal static UnitAliasParsingData Empty { get; } = new();

    private UnitAliasParsingData() : base(UnitAliasLocations.Empty) { }
}