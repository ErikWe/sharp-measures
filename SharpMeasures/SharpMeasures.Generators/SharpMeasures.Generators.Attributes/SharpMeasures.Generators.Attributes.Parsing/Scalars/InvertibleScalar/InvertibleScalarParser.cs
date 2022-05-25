namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class InvertibleScalarParser
{
    public static IAttributeParser<InvertibleScalarDefinition> Parser { get; } = new AttributeParser();

    private static InvertibleScalarDefinition DefaultDefinition() => InvertibleScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<InvertibleScalarDefinition, InvertibleScalarParsingData, InvertibleScalarLocations, InvertibleScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, InvertibleScalarProperties.AllProperties) { }
    }
}
