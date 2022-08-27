namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitAliasLocations : ADependantUnitLocations<UnitAliasLocations>
{
    public static UnitAliasLocations Empty { get; } = new();

    public MinimalLocation? AliasOf => DependantOn;

    public bool ExplicitlySetAliasOf => ExplicitlySetDependantOn;

    protected override UnitAliasLocations Locations => this;

    private UnitAliasLocations() { }

}
