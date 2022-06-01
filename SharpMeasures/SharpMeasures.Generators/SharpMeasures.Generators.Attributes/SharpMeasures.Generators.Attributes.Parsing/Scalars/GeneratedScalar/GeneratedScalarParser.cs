namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedScalarDefinition> Parser { get; } = new AttributeParser();

    private static RawGeneratedScalarDefinition DefaultDefiniton() => RawGeneratedScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedScalarDefinition, GeneratedScalarLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedScalarProperties.AllProperties) { }
    }
}
