namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class GeneratedUnitParser
{
    public static IAttributeParser<GeneratedScalarDefinition> Parser { get; } = new AttributeParser();

    private static GeneratedScalarDefinition DefaultDefiniton() => GeneratedScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<GeneratedScalarDefinition, GeneratedScalarParsingData, GeneratedScalarLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedScalarProperties.AllProperties) { }
    }
}
