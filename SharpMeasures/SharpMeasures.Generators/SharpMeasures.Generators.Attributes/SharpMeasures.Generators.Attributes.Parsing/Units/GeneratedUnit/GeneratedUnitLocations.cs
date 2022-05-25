namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class GeneratedUnitLocations : AAttributeLocations
{
    internal static GeneratedUnitLocations Empty { get; } = new();

    public MinimalLocation Quantity { get; init; }

    public MinimalLocation AllowBias { get; init; }

    public MinimalLocation GenerateDocumentation { get; init; }

    private GeneratedUnitLocations() { }
}