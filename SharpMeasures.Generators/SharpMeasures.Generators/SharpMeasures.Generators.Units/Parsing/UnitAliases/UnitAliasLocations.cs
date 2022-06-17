namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitAliasLocations : ADependantUnitLocations
{
    public static UnitAliasLocations Empty { get; } = new();

    public MinimalLocation? AliasOf => DependantOn;

    public bool ExplicitlySetAliasOf => AliasOf is not null;

    private UnitAliasLocations() { }
}
