namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawUnitAliasDefinition : ARawDependantUnitDefinition<UnitAliasParsingData, UnitAliasLocations>
{
    internal static RawUnitAliasDefinition Empty { get; } = new();

    public string? AliasOf => DependantOn;

    private RawUnitAliasDefinition() : base(UnitAliasLocations.Empty, UnitAliasParsingData.Empty) { }
}