namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedScalar> Parser { get; } = new AttributeParser();

    private static RawGeneratedScalar DefaultDefiniton() => RawGeneratedScalar.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedScalar, GeneratedScalarLocations, GeneratedScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedScalarProperties.AllProperties) { }
    }
}
