namespace SharpMeasures.Generators.Vectors.Parsing.VectorConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class VectorConstantParser
{
    public static IAttributeParser<RawVectorConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawVectorConstantDefinition DefaultDefiniton() => RawVectorConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawVectorConstantDefinition, VectorConstantLocations, VectorConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, VectorConstantProperties.AllProperties) { }
    }
}
