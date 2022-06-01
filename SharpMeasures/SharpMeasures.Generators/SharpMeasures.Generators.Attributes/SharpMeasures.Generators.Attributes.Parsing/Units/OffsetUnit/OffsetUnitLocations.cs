namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class OffsetUnitLocations : ADependantUnitLocations
{
    internal static OffsetUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Offset { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetOffset => Offset is not null;

    private OffsetUnitLocations() { }
}