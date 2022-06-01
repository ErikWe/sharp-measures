namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class ScaledUnitLocations : ADependantUnitLocations
{
    internal static ScaledUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Scale { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetScale => Scale is not null;

    private ScaledUnitLocations() { }
}