namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnprocessedUnitAliasDefinition : AUnprocessedDependantUnitDefinition<UnprocessedUnitAliasDefinition, UnitAliasLocations>
{
    public static UnprocessedUnitAliasDefinition Empty { get; } = new();

    public string? AliasOf => DependantOn;

    protected override UnprocessedUnitAliasDefinition Definition => this;

    private UnprocessedUnitAliasDefinition() : base(UnitAliasLocations.Empty) { }
}
