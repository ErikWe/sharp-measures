namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class GeneratedUnitDefinition : AAttributeDefinition<GeneratedUnitLocations>
{
    public NamedType Quantity { get; }

    public bool SupportsBiasedQuantities { get; }

    public bool GenerateDocumentation { get; }

    public GeneratedUnitDefinition(NamedType quantity, bool supportsBiasedQuantities, bool generateDocumentation, GeneratedUnitLocations locations)
        : base(locations)
    {
        Quantity = quantity;

        SupportsBiasedQuantities = supportsBiasedQuantities;
        GenerateDocumentation = generateDocumentation;
    }
}