namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawGeneratedUnitDefinition : ARawAttributeDefinition<GeneratedUnitLocations>
{
    public static RawGeneratedUnitDefinition Empty { get; } = new();

    public NamedType? Quantity { get; init; }

    public bool SupportsBiasedQuantities { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawGeneratedUnitDefinition() : base(GeneratedUnitLocations.Empty) { }
}
