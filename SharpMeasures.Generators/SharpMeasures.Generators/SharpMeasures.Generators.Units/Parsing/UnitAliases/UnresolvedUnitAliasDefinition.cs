namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;

internal record class UnresolvedUnitAliasDefinition : AUnresolvedDependantUnitDefinition<UnitAliasLocations>, IUnresolvedUnitAlias
{
    public string AliasOf => DependantOn;

    public UnresolvedUnitAliasDefinition(string name, string plural, string aliasOf, UnitAliasLocations locations) : base(name, plural, aliasOf, locations) { }
}
