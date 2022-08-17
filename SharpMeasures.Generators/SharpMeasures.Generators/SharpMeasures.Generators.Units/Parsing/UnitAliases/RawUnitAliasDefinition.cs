namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class RawUnitAliasDefinition : ARawDependantUnitDefinition<UnitAliasLocations>, IRawUnitAlias
{
    public string AliasOf => DependantOn;

    public RawUnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
