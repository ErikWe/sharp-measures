namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Units.UnitInstances;

internal record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasLocations>, IUnitAlias
{
    public IRawUnitInstance AliasOf => DependantOn;

    public UnitAliasDefinition(string name, string plural, IRawUnitInstance aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
