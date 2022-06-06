namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpMeasures.Generators.Vectors;

public static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedVector> Parser { get; } = new AttributeParser();

    private static RawGeneratedVector DefaultDefiniton() => RawGeneratedVector.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedVector, GeneratedVectorLocations, GeneratedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedVectorProperties.AllProperties) { }
    }
}
