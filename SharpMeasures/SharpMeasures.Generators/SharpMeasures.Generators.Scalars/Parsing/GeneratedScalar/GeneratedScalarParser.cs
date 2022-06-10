namespace SharpMeasures.Generators.Scalars.Parsing.GeneratedScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedScalarDefinition> Parser { get; } = new AttributeParser();

    private static RawGeneratedScalarDefinition DefaultDefiniton() => RawGeneratedScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedScalarDefinition, GeneratedScalarLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedScalarProperties.AllProperties) { }
    }
}
