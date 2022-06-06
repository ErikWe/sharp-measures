namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAlias : ADependantUnitDefinition<UnitAliasLocations>
{
    public string AliasOf => DependantOn;

    public UnitAlias(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
