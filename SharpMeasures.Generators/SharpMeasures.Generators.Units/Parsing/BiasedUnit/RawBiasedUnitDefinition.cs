namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class RawBiasedUnitDefinition : ARawDependantUnitDefinition<RawBiasedUnitDefinition, BiasedUnitLocations>
{
    internal static RawBiasedUnitDefinition Empty { get; } = new();

    public string? From => DependantOn;
    public double Bias { get; init; }
    public string? Expression { get; init; }

    protected override RawBiasedUnitDefinition Definition => this;

    private RawBiasedUnitDefinition() : base(BiasedUnitLocations.Empty) { }
}
