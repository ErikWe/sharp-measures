namespace SharpMeasures.Generators.Units.Parsing.SharpMeasuresUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Raw.Scalars;

internal record class SharpMeasuresUnitDefinition : ASharpMeasuresObjectDefinition<SharpMeasuresUnitLocations>, IUnit
{
    public IRawScalarType Quantity { get; }

    public bool BiasTerm { get; }

    public SharpMeasuresUnitDefinition(IRawScalarType quantity, bool biasTerm, bool? generateDocumentation, SharpMeasuresUnitLocations locations) : base(generateDocumentation, locations)
    {
        Quantity = quantity;

        BiasTerm = biasTerm;
    }
}
