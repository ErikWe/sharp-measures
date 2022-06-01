namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class UnitAliasLocations : ADependantUnitLocations
{
    internal static UnitAliasLocations Empty { get; } = new();

    public MinimalLocation? AliasOf => DependantOn;

    public bool ExplicitlySetAliasOf => AliasOf is not null;

    private UnitAliasLocations() { }
}