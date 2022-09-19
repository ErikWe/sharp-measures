namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal sealed record class RawBiasedUnitInstanceDefinition : ARawModifiedUnitDefinition<RawBiasedUnitInstanceDefinition, BiasedUnitInstanceLocations>
{
    internal static RawBiasedUnitInstanceDefinition Empty { get; } = new();

    public double? Bias { get; init; }
    public string? Expression { get; init; }

    protected override RawBiasedUnitInstanceDefinition Definition => this;

    private RawBiasedUnitInstanceDefinition() : base(BiasedUnitInstanceLocations.Empty) { }
}
