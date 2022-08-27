namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawFixedUnitDefinition : ARawUnitDefinition<RawFixedUnitDefinition, FixedUnitLocations>
{
    public static RawFixedUnitDefinition Empty { get; } = new();

    protected override RawFixedUnitDefinition Definition => this;

    private RawFixedUnitDefinition() : base(FixedUnitLocations.Empty) { }
}
