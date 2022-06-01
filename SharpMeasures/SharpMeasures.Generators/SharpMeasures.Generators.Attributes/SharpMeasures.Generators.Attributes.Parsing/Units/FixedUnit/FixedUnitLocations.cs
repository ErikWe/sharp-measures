namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class FixedUnitLocations : AUnitLocations
{
    internal static FixedUnitLocations Empty { get; } = new();

    public MinimalLocation? Value { get; init; }
    public MinimalLocation? Bias { get; init; }

    public bool ExplicitlySetValue => Value is not null;
    public bool ExplicitlySetBias => Bias is not null;

    private FixedUnitLocations() { }
}