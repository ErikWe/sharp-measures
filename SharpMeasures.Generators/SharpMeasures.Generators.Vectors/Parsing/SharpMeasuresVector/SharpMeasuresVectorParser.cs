namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorParser
{
    public static IAttributeParser<RawSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawSharpMeasuresVectorDefinition DefaultDefiniton() => RawSharpMeasuresVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations, SharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorProperties.AllProperties) { }
    }
}
