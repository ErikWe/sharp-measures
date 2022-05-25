namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasParsingData, UnitAliasLocations>
{
    internal static UnitAliasDefinition Empty { get; } = new();

    public string AliasOf => DependantOn;

    private UnitAliasDefinition() : base(UnitAliasParsingData.Empty) { }
}