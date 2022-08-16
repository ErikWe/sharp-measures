namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnprocessedScaledUnitDefinition : AUnprocessedDependantUnitDefinition<UnprocessedScaledUnitDefinition, ScaledUnitLocations>
{
    public static UnprocessedScaledUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Scale { get; init; }
    public string? Expression { get; init; }

    protected override UnprocessedScaledUnitDefinition Definition => this;

    private UnprocessedScaledUnitDefinition() : base(ScaledUnitLocations.Empty) { }
}
