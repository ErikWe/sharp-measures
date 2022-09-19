namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorParser
{
    public static IAttributeParser<SymbolicSharpMeasuresVectorDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresVectorDefinition DefaultDefiniton() => SymbolicSharpMeasuresVectorDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicSharpMeasuresVectorDefinition, SharpMeasuresVectorLocations, SharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorProperties.AllProperties) { }
    }
}
