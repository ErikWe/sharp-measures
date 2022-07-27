namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorGroupParser
{
    public static IAttributeParser<RawSharpMeasuresVectorGroupDefinition> Parser { get; } = new AttributeParser();

    private static RawSharpMeasuresVectorGroupDefinition DefaultDefiniton() => RawSharpMeasuresVectorGroupDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations, SharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorGroupProperties.AllProperties) { }
    }
}
