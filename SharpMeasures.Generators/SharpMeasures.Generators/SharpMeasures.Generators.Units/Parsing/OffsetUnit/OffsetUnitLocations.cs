namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class OffsetUnitLocations : ADependantUnitLocations
{
    public static OffsetUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Offset { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetOffset => Offset is not null;

    private OffsetUnitLocations() { }
}
