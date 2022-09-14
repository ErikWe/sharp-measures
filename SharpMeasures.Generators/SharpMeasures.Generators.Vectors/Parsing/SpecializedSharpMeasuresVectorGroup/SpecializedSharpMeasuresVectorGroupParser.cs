namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresVectorGroupParser
{
    public static IAttributeParser<SymbolicSpecializedSharpMeasuresVectorGroupDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSpecializedSharpMeasuresVectorGroupDefinition DefaultDefiniton() => SymbolicSpecializedSharpMeasuresVectorGroupDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicSpecializedSharpMeasuresVectorGroupDefinition, SpecializedSharpMeasuresVectorGroupLocations, SpecializedSharpMeasuresVectorGroupAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresVectorGroupProperties.AllProperties) { }
    }
}
