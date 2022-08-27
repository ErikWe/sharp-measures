namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class FixedUnitLocations : AUnitLocations<FixedUnitLocations>
{
    public static FixedUnitLocations Empty { get; } = new();

    protected override FixedUnitLocations Locations => this;

    private FixedUnitLocations() { }
}
