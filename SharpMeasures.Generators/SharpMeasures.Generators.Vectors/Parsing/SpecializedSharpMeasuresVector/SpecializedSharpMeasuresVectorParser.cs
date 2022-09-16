namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresVectorParser
{
    public static IAttributeParser<SymbolicSpecializedSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSpecializedSharpMeasuresVectorDefinition DefaultDefiniton() => SymbolicSpecializedSharpMeasuresVectorDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicSpecializedSharpMeasuresVectorDefinition, SpecializedSharpMeasuresVectorLocations, SpecializedSharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresVectorProperties.AllProperties) { }
    }
}
