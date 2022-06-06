namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawGeneratedUnit : ARawAttributeDefinition<GeneratedUnitLocations>
{
    internal static RawGeneratedUnit Empty { get; } = new();

    public NamedType? Quantity { get; init; }

    public bool SupportsBiasedQuantities { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedUnit() : base(GeneratedUnitLocations.Empty) { }
}
