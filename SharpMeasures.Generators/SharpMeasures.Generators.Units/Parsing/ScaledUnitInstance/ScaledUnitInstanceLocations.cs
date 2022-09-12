namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class ScaledUnitInstanceLocations : AModifiedUnitInstanceLocations<ScaledUnitInstanceLocations>, IScaledUnitInstanceLocations
{
    internal static ScaledUnitInstanceLocations Empty { get; } = new();

    public MinimalLocation? Scale { get; init; }
    public MinimalLocation? Expression { get; init; }

    public bool ExplicitlySetScale => Scale is not null;
    public bool ExplicitlySetExpression => Expression is not null;

    protected override ScaledUnitInstanceLocations Locations => this;

    private ScaledUnitInstanceLocations() { }
}
