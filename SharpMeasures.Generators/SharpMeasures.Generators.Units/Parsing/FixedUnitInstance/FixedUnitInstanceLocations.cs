namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitInstanceLocations : AUnitInstanceLocations<FixedUnitInstanceLocations>, IFixedUnitInstanceLocations
{
    public static FixedUnitInstanceLocations Empty { get; } = new();

    protected override FixedUnitInstanceLocations Locations => this;

    private FixedUnitInstanceLocations() { }
}
