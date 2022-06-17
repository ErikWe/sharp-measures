namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedUnitDefinition : AAttributeDefinition<GeneratedUnitLocations>
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public bool GenerateDocumentation { get; }

    public GeneratedUnitDefinition(NamedType quantity, bool biasTerm, bool generateDocumentation, GeneratedUnitLocations locations)
        : base(locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
        GenerateDocumentation = generateDocumentation;
    }
}
