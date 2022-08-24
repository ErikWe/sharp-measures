namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units;

internal record class SharpMeasuresUnitDefinition : ASharpMeasuresObjectDefinition<SharpMeasuresUnitLocations>, IUnit
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public SharpMeasuresUnitDefinition(NamedType quantity, bool biasTerm, bool? generateDocumentation, SharpMeasuresUnitLocations locations) : base(generateDocumentation, locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
