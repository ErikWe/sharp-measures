namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;

internal record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasLocations>, IUnitAlias
{
    public string AliasOf => DependantOn;

    public UnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
