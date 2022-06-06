namespace SharpMeasures.Generators.Attributes.Parsing.Vectors;

using SharpMeasures.Generators.Vectors;

public static class ResizedVectorParser
{
    public static IAttributeParser<RawResizedVector> Parser { get; } = new AttributeParser();

    private static RawResizedVector DefaultDefiniton() => RawResizedVector.Empty;

    private class AttributeParser : AAttributeParser<RawResizedVector, ResizedVectorLocations, ResizedVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ResizedVectorProperties.AllProperties) { }
    }
}
