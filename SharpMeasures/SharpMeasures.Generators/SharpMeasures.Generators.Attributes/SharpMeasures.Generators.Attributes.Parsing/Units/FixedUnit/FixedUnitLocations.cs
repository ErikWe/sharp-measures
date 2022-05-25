namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitLocations : AUnitLocations
{
    internal static FixedUnitLocations Empty { get; } = new();

    public MinimalLocation Value { get; init; }
    public MinimalLocation Bias { get; init; }

    private FixedUnitLocations() { }
}