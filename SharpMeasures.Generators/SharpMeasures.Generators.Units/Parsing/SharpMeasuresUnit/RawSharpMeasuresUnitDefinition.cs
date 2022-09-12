namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresUnitDefinition : ARawSharpMeasuresObjectDefinition<RawSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>
{
    public static RawSharpMeasuresUnitDefinition Empty { get; } = new(SharpMeasuresUnitLocations.Empty);

    protected override RawSharpMeasuresUnitDefinition Definition => this;

    public NamedType? Quantity { get; init; }

    public bool BiasTerm { get; init; }

    private RawSharpMeasuresUnitDefinition(SharpMeasuresUnitLocations locations) : base(locations) { }
}
