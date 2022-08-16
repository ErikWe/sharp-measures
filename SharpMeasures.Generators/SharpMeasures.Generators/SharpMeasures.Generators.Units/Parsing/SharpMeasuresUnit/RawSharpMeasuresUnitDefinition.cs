namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Units;

internal record class RawSharpMeasuresUnitDefinition : ASharpMeasuresObjectDefinition<SharpMeasuresUnitLocations>, IRawUnit
{
    public NamedType Quantity { get; }

    public bool BiasTerm { get; }

    public RawSharpMeasuresUnitDefinition(NamedType quantity, bool biasTerm, bool? generateDocumentation, SharpMeasuresUnitLocations locations) : base(generateDocumentation, locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
