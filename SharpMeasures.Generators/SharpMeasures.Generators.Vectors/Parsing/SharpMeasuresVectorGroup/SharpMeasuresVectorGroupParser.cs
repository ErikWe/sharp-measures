namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorGroupParser
{
    public static IAttributeParser<SymbolicSharpMeasuresVectorGroupDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresVectorGroupDefinition DefaultDefiniton() => SymbolicSharpMeasuresVectorGroupDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicSharpMeasuresVectorGroupDefinition, SharpMeasuresVectorGroupLocations, VectorGroupAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorGroupProperties.AllProperties) { }
    }
}
