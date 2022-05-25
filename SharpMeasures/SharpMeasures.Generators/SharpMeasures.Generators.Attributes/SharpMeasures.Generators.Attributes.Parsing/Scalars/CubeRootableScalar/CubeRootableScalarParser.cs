namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class CubeRootableScalarParser
{
    public static IAttributeParser<CubeRootableScalarDefinition> Parser { get; } = new AttributeParser();

    private static CubeRootableScalarDefinition DefaultDefinition() => CubeRootableScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<CubeRootableScalarDefinition, CubeRootableScalarParsingData, CubeRootableScalarLocations, CubeRootableScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, CubeRootableScalarProperties.AllProperties) { }
    }
}
