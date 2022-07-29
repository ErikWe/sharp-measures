namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresVectorGroupParser
{
    public static IAttributeParser<RawSpecializedSharpMeasuresVectorGroupDefinition> Parser { get; } = new AttributeParser();

    private static RawSpecializedSharpMeasuresVectorGroupDefinition DefaultDefiniton() => RawSpecializedSharpMeasuresVectorGroupDefinition.Empty;

    private class AttributeParser
        : AAttributeParser<RawSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations, SpecializedSharpMeasuresVectorGroupAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresVectorGroupProperties.AllProperties) { }
    }
}
