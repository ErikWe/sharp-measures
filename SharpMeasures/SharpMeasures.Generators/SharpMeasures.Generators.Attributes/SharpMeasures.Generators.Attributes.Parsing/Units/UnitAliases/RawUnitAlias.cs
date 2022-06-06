namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawUnitAlias : ARawDependantUnitDefinition<UnitAliasParsingData, UnitAliasLocations>
{
    internal static RawUnitAlias Empty { get; } = new();

    public string? AliasOf => DependantOn;

    private RawUnitAlias() : base(UnitAliasLocations.Empty, UnitAliasParsingData.Empty) { }
}
