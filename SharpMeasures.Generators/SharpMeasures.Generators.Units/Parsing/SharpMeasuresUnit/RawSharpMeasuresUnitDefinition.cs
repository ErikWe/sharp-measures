namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresUnitDefinition : ARawSharpMeasuresObjectDefinition<RawSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>
{
    public static RawSharpMeasuresUnitDefinition FromSymbolic(SymbolicSharpMeasuresUnitDefinition symbolicDefinition) => new RawSharpMeasuresUnitDefinition(symbolicDefinition.Locations) with
    {
        Quantity = symbolicDefinition.Quantity?.AsNamedType(),
        BiasTerm = symbolicDefinition.BiasTerm,
        GenerateDocumentation = symbolicDefinition.GenerateDocumentation
    };

    protected override RawSharpMeasuresUnitDefinition Definition => this;

    public NamedType? Quantity { get; private init; }

    public bool BiasTerm { get; private init; }

    private RawSharpMeasuresUnitDefinition(SharpMeasuresUnitLocations locations) : base(locations) { }
}
