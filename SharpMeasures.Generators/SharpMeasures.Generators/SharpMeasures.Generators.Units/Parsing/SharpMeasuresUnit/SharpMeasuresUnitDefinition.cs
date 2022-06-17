namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresUnitDefinition : AAttributeDefinition<SharpMeasuresUnitLocations>
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public bool GenerateDocumentation { get; }

    public SharpMeasuresUnitDefinition(NamedType quantity, bool biasTerm, bool generateDocumentation, SharpMeasuresUnitLocations locations)
        : base(locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
        GenerateDocumentation = generateDocumentation;
    }
}
