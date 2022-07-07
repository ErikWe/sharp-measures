namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class UnitAliasDefinition : ADependantUnitDefinition<UnitAliasLocations>, IUnitAlias
{
    public IUnresolvedUnitInstance AliasOf => DependantOn;

    public UnitAliasDefinition(string name, string plural, IUnresolvedUnitInstance aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
