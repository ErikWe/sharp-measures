namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Scalars;

internal record class SharpMeasuresUnitDefinition : IAttributeDefinition<SharpMeasuresUnitLocations>, IUnit
{
    public IUnresolvedScalarType Quantity { get; }

    public bool BiasTerm { get; }

    public bool GenerateDocumentation { get; }

    public SharpMeasuresUnitLocations Locations { get; }

    public SharpMeasuresUnitDefinition(IUnresolvedScalarType quantity, bool biasTerm, bool generateDocumentation, SharpMeasuresUnitLocations locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
        GenerateDocumentation = generateDocumentation;

        Locations = locations;
    }
}
