namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class GeneratedUnitDefinition : AAttributeDefinition<GeneratedUnitParsingData, GeneratedUnitLocations>
{
    internal static GeneratedUnitDefinition Empty { get; } = new();

    public NamedType Quantity { get; init; }

    public bool AllowBias { get; init; }

    public bool GenerateDocumentation { get; init; }

    private GeneratedUnitDefinition() : base(GeneratedUnitParsingData.Empty) { }
}