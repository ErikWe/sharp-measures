namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresUnitDefinition : ARawAttributeDefinition<SharpMeasuresUnitLocations>
{
    public static RawSharpMeasuresUnitDefinition Empty { get; } = new();

    public NamedType? Quantity { get; init; }

    public bool BiasTerm { get; init; }

    public bool GenerateDocumentation { get; init; }

    private RawSharpMeasuresUnitDefinition() : base(SharpMeasuresUnitLocations.Empty) { }
}
