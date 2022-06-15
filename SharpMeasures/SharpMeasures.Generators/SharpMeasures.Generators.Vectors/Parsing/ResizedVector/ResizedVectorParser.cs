namespace SharpMeasures.Generators.Vectors.Parsing.ResizedVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ResizedVectorParser
{
    public static IAttributeParser<RawResizedVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawResizedVectorDefinition DefaultDefiniton() => RawResizedVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawResizedVectorDefinition, ResizedVectorLocations, ResizedSharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ResizedVectorProperties.AllProperties) { }
    }
}
