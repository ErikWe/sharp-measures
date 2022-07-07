namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class BiasedUnitLocations : ADependantUnitLocations<BiasedUnitLocations>
{
    public static BiasedUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Bias { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetFrom => ExplicitlySetDependantOn;
    public bool ExplicitlySetBias => Bias is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    protected override BiasedUnitLocations Locations => this;

    private BiasedUnitLocations() { }
}
