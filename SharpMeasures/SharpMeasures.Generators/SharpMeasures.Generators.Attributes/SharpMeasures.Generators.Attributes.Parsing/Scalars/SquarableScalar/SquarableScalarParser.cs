namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class SquarableScalarParser
{
    public static IAttributeParser<SquarableScalarDefinition> Parser { get; } = new AttributeParser();

    private static SquarableScalarDefinition DefaultDefinition() => SquarableScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<SquarableScalarDefinition, SquarableScalarParsingData, SquarableScalarLocations, SquarableScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, SquarableScalarProperties.AllProperties) { }
    }
}
