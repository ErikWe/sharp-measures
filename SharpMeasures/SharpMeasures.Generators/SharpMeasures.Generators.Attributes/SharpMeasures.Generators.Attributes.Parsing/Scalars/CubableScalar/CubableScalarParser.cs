namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class CubableScalarParser
{
    public static IAttributeParser<CubableScalarDefinition> Parser { get; } = new AttributeParser();

    private static CubableScalarDefinition DefaultDefinition() => CubableScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<CubableScalarDefinition, CubableScalarParsingData, CubableScalarLocations, CubableScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, CubableScalarProperties.AllProperties) { }
    }
}
