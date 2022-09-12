namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawScaledUnitInstanceDefinition : ARawModifiedUnitDefinition<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations>
{
    public static RawScaledUnitInstanceDefinition Empty { get; } = new();

    public double? Scale { get; init; }
    public string? Expression { get; init; }

    protected override RawScaledUnitInstanceDefinition Definition => this;

    private RawScaledUnitInstanceDefinition() : base(ScaledUnitInstanceLocations.Empty) { }
}
