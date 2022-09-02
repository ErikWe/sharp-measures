namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitInstanceAliasDefinition : ADependantUnitInstance<UnitInstanceAliasLocations>, IUnitInstanceAlias
{
    IUnitInstanceAliasLocations IUnitInstanceAlias.Locations => Locations;

    public UnitInstanceAliasDefinition(string name, string pluralForm, string originalUnitInstance, UnitInstanceAliasLocations locations) : base(name, pluralForm, originalUnitInstance, locations) { }
}
