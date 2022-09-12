namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresVectorParser
{
    public static IAttributeParser<RawSpecializedSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawSpecializedSharpMeasuresVectorDefinition DefaultDefiniton() => RawSpecializedSharpMeasuresVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations, SpecializedSharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresVectorProperties.AllProperties) { }
    }
}
