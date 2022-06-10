namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class GeneratedUnitLocations : AAttributeLocations
{
    public static GeneratedUnitLocations Empty { get; } = new();

    public MinimalLocation? Quantity { get; init; }

    public MinimalLocation? SupportsBiasedQuantities { get; init; }
    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetQuantity => Quantity is not null;
    public bool ExplicitlySetSupportsBiasedQuantities => SupportsBiasedQuantities is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private GeneratedUnitLocations() { }
}
