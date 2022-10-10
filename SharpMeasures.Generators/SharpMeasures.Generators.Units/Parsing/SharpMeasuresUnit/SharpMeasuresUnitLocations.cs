namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresUnitLocations : AAttributeLocations<SharpMeasuresUnitLocations>, IUnitLocations
{
    public static SharpMeasuresUnitLocations Empty { get; } = new();

    public MinimalLocation? Quantity { get; init; }

    public MinimalLocation? BiasTerm { get; init; }

    public bool ExplicitlySetQuantity => Quantity is not null;

    public bool ExplicitlySetBiasTerm => BiasTerm is not null;

    protected override SharpMeasuresUnitLocations Locations => this;

    private SharpMeasuresUnitLocations() { }
}
