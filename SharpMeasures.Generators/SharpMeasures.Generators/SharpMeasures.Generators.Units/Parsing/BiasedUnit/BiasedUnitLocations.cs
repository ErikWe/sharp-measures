namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitLocations : ADependantUnitLocations
{
    public static BiasedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Bias { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetBias => Bias is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    private BiasedUnitLocations() { }
}
