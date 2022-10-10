namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal sealed record class SharpMeasuresUnitDefinition : AAttributeDefinition<SharpMeasuresUnitLocations>, IUnit
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    ISharpMeasuresObjectLocations ISharpMeasuresObject.Locations => Locations;
    IUnitLocations IUnit.Locations => Locations;

    public SharpMeasuresUnitDefinition(NamedType quantity, bool biasTerm, SharpMeasuresUnitLocations locations) : base(locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
