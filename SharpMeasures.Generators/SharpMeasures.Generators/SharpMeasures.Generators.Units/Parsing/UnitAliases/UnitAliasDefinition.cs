namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasLocations>
{
    public string AliasOf => DependantOn;

    public UnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
