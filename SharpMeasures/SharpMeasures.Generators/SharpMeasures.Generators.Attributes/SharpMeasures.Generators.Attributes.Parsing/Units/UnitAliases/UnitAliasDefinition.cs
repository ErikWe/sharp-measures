namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasLocations>
{
    public string AliasOf => DependantOn;

    public UnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}