namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class UnresolvedUnitAliasDefinition : ARawDependantUnitDefinition<UnitAliasLocations>, IRawUnitAlias
{
    public string AliasOf => DependantOn;

    public UnresolvedUnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
