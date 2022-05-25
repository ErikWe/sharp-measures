namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class SquareRootableScalarParser
{
    public static IAttributeParser<SquareRootableScalarDefinition> Parser { get; } = new AttributeParser();

    private static SquareRootableScalarDefinition DefaultDefinition() => SquareRootableScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<SquareRootableScalarDefinition, SquareRootableScalarParsingData, SquareRootableScalarLocations, SquareRootableScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, SquareRootableScalarProperties.AllProperties) { }
    }
}
