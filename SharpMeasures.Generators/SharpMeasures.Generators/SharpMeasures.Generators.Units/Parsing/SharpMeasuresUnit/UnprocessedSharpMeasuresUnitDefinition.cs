namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class UnprocessedSharpMeasuresUnitDefinition : AUnprocessedSharpMeasuresObjectDefinition<UnprocessedSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>
{
    public static UnprocessedSharpMeasuresUnitDefinition Empty { get; } = new(SharpMeasuresUnitLocations.Empty);

    protected override UnprocessedSharpMeasuresUnitDefinition Definition => this;

    public NamedType? Quantity { get; init; }

    public bool BiasTerm { get; init; }

    private UnprocessedSharpMeasuresUnitDefinition(SharpMeasuresUnitLocations locations) : base(locations) { }
}
