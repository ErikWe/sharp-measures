namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitLocations : ADependantUnitLocations<ScaledUnitLocations>
{
    internal static ScaledUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Scale { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetFrom => ExplicitlySetDependantOn;
    public bool ExplicitlySetScale => Scale is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    protected override ScaledUnitLocations Locations => this;

    private ScaledUnitLocations() { }
}
