namespace SharpMeasures.Generators.Units.Parsing.BiasedUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitInstanceLocations : AModifiedUnitInstanceLocations<BiasedUnitInstanceLocations>, IBiasedUnitInstanceLocations
{
    public static BiasedUnitInstanceLocations Empty { get; } = new();

    public MinimalLocation? Bias { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetBias => Bias is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    protected override BiasedUnitInstanceLocations Locations => this;

    private BiasedUnitInstanceLocations() { }
}
