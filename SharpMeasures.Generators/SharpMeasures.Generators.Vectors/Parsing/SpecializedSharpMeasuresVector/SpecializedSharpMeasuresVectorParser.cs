namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresVectorParser
{
    public static IAttributeParser<SymbolicSpecializedSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSpecializedSharpMeasuresVectorDefinition DefaultDefiniton() => SymbolicSpecializedSharpMeasuresVectorDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations, SpecializedVectorQuantityAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresVectorProperties.AllProperties) { }
    }
}
