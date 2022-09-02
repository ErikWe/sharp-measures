namespace SharpMeasures.Generators.Units.Parsing.FixedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawFixedUnitInstanceDefinition : ARawUnitInstance<RawFixedUnitInstanceDefinition, FixedUnitInstanceLocations>
{
    public static RawFixedUnitInstanceDefinition Empty { get; } = new();

    protected override RawFixedUnitInstanceDefinition Definition => this;

    private RawFixedUnitInstanceDefinition() : base(FixedUnitInstanceLocations.Empty) { }
}
