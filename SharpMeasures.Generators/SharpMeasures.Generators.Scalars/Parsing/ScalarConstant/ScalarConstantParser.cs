namespace SharpMeasures.Generators.Scalars.Parsing.ScalarConstant;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ScalarConstantParser
{
    public static IAttributeParser<RawScalarConstantDefinition> Parser { get; } = new AttributeParser();

    private static RawScalarConstantDefinition DefaultDefiniton() => RawScalarConstantDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawScalarConstantDefinition, ScalarConstantLocations, ScalarConstantAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, ScalarConstantProperties.AllProperties) { }
    }
}
