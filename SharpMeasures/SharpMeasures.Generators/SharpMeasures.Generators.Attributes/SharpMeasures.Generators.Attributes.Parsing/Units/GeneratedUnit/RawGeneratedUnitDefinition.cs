namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class RawGeneratedUnitDefinition : ARawAttributeDefinition<GeneratedUnitLocations>
{
    internal static RawGeneratedUnitDefinition Empty { get; } = new();

    public NamedType? Quantity { get; init; }

    public bool SupportsBiasedQuantities { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedUnitDefinition() : base(GeneratedUnitLocations.Empty) { }
}