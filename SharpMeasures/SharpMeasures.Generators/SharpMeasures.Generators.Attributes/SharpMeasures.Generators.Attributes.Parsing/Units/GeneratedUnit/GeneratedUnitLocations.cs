namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class GeneratedUnitLocations : AAttributeLocations
{
    internal static GeneratedUnitLocations Empty { get; } = new();

    public MinimalLocation? Quantity { get; init; }

    public MinimalLocation? SupportsBiasedQuantities { get; init; }
    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetQuantity => Quantity is not null;
    public bool ExplicitlySetSupportsBiasedQuantities => SupportsBiasedQuantities is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private GeneratedUnitLocations() { }
}