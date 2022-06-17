namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitLocations : AUnitLocations
{
    public static FixedUnitLocations Empty { get; } = new();

    public MinimalLocation? Value { get; init; }
    public MinimalLocation? Bias { get; init; }

    public bool ExplicitlySetValue => Value is not null;
    public bool ExplicitlySetBias => Bias is not null;

    private FixedUnitLocations() { }
}
