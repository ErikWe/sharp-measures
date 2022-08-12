namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal record class RawSharpMeasuresUnitDefinition : IOpenAttributeDefinition<RawSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>
{
    public static RawSharpMeasuresUnitDefinition Empty { get; } = new(SharpMeasuresUnitLocations.Empty);

    public NamedType? Quantity { get; init; }

    public bool BiasTerm { get; init; }

    public bool? GenerateDocumentation { get; init; }

    public SharpMeasuresUnitLocations Locations { get; private init; }

    private RawSharpMeasuresUnitDefinition(SharpMeasuresUnitLocations locations)
    {
        Locations = locations;
    }

    protected RawSharpMeasuresUnitDefinition WithLocations(SharpMeasuresUnitLocations locations) => this with
    {
        Locations = locations
    };

    RawSharpMeasuresUnitDefinition IOpenAttributeDefinition<RawSharpMeasuresUnitDefinition, SharpMeasuresUnitLocations>.WithLocations(SharpMeasuresUnitLocations locations)
        => WithLocations(locations);
}
