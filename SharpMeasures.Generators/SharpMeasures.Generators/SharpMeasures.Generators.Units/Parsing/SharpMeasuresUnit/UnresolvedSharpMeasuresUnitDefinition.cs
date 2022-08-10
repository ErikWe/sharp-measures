namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Units;

internal record class UnresolvedSharpMeasuresUnitDefinition : IAttributeDefinition<SharpMeasuresUnitLocations>, IUnresolvedUnit
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public bool? GenerateDocumentation { get; }

    public SharpMeasuresUnitLocations Locations { get; }

    public UnresolvedSharpMeasuresUnitDefinition(NamedType quantity, bool biasTerm, bool? generateDocumentation, SharpMeasuresUnitLocations locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
        GenerateDocumentation = generateDocumentation;

        Locations = locations;
    }
}
