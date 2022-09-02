namespace SharpMeasures.Generators.Units.Parsing.UnitInstanceAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawUnitInstanceAliasDefinition : ARawModifiedUnitDefinition<RawUnitInstanceAliasDefinition, UnitInstanceAliasLocations>
{
    public static RawUnitInstanceAliasDefinition Empty { get; } = new();

    protected override RawUnitInstanceAliasDefinition Definition => this;

    private RawUnitInstanceAliasDefinition() : base(UnitInstanceAliasLocations.Empty) { }
}
