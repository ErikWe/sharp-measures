namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class SharpMeasuresUnitLocations : AAttributeLocations
{
    public static SharpMeasuresUnitLocations Empty { get; } = new();

    public MinimalLocation? Quantity { get; init; }

    public MinimalLocation? BiasTerm { get; init; }
    public MinimalLocation? GenerateDocumentation { get; init; }

    public bool ExplicitlySetQuantity => Quantity is not null;
    public bool ExplicitlySetSupportsBiasedQuantities => BiasTerm is not null;
    public bool ExplicitlySetGenerateDocumentation => GenerateDocumentation is not null;

    private SharpMeasuresUnitLocations() { }
}
