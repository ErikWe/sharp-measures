namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class UnprocessedBiasedUnitDefinition : AUnprocessedDependantUnitDefinition<UnprocessedBiasedUnitDefinition, BiasedUnitLocations>
{
    internal static UnprocessedBiasedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Bias { get; init; }
    public string? Expression { get; init; }

    protected override UnprocessedBiasedUnitDefinition Definition => this;

    private UnprocessedBiasedUnitDefinition() : base(BiasedUnitLocations.Empty) { }
}
