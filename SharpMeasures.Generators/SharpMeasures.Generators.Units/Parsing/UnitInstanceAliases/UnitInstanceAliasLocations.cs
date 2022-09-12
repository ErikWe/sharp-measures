namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnitInstanceAliasLocations : AModifiedUnitInstanceLocations<UnitInstanceAliasLocations>, IUnitInstanceAliasLocations
{
    public static UnitInstanceAliasLocations Empty { get; } = new();

    protected override UnitInstanceAliasLocations Locations => this;

    private UnitInstanceAliasLocations() { }
}
