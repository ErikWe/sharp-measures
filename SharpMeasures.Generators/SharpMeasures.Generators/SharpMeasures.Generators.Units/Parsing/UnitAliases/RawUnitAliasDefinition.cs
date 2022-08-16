namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawUnitAliasDefinition : AUnprocessedDependantUnitDefinition<RawUnitAliasDefinition, UnitAliasLocations>
{
    public static RawUnitAliasDefinition Empty { get; } = new();

    public string? AliasOf => DependantOn;

    protected override RawUnitAliasDefinition Definition => this;

    private RawUnitAliasDefinition() : base(UnitAliasLocations.Empty) { }
}
