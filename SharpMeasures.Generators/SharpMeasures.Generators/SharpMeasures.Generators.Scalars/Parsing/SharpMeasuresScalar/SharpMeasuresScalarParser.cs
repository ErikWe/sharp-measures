namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresScalarParser
{
    public static IAttributeParser<UnprocessedSharpMeasuresScalarDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedSharpMeasuresScalarDefinition DefaultDefiniton() => UnprocessedSharpMeasuresScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations, SharpMeasuresScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresScalarProperties.AllProperties) { }
    }
}
