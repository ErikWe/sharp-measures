namespace SharpMeasures.Generators.Units.Parsing.UnitAlias;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawUnitAliasDefinition : ARawDependantUnitDefinition<UnitAliasParsingData, UnitAliasLocations>
{
    public static RawUnitAliasDefinition Empty { get; } = new();

    public string? AliasOf => DependantOn;

    private RawUnitAliasDefinition() : base(UnitAliasLocations.Empty, UnitAliasParsingData.Empty) { }
}
