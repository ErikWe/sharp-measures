namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitLocations : ADependantUnitLocations
{
    internal static ScaledUnitLocations Empty { get; } = new();

    public MinimalLocation? From => DependantOn;
    public MinimalLocation? Scale { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetFrom => From is not null;
    public bool ExplicitlySetScale => Scale is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    private ScaledUnitLocations() { }
}
