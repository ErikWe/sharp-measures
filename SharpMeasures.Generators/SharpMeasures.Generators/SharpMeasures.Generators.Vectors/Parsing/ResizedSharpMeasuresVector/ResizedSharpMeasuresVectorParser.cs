namespace SharpMeasures.Generators.Vectors.Parsing.ResizedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ResizedSharpMeasuresVectorParser
{
    public static IAttributeParser<RawResizedSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawResizedSharpMeasuresVectorDefinition DefaultDefiniton() => RawResizedSharpMeasuresVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawResizedSharpMeasuresVectorDefinition, ResizedSharpMeasuresVectorLocations, ResizedSharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ResizedSharpMeasuresVectorProperties.AllProperties) { }
    }
}
