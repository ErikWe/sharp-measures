namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawScaledUnitDefinition : ARawDependantUnitDefinition<RawScaledUnitDefinition, ScaledUnitLocations>
{
    public static RawScaledUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Scale { get; init; }
    public string? Expression { get; init; }

    protected override RawScaledUnitDefinition Definition => this;

    private RawScaledUnitDefinition() : base(ScaledUnitLocations.Empty) { }
}
