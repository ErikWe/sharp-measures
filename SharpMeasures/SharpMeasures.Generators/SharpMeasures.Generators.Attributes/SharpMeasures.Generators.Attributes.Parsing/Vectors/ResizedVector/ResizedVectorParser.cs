namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpMeasures.Generators.Vectors;

public static class ResizedVectorParser
{
    public static IAttributeParser<RawResizedVectorDefinition> Parser { get; } = new AttributeParser();

    private static RawResizedVectorDefinition DefaultDefiniton() => RawResizedVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawResizedVectorDefinition, ResizedVectorLocations, ResizedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ResizedVectorProperties.AllProperties) { }
    }
}
