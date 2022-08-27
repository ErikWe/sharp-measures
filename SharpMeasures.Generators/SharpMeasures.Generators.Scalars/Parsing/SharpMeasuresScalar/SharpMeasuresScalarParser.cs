namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresScalarParser
{
    public static IAttributeParser<RawSharpMeasuresScalarDefinition> Parser { get; } = new AttributeParser();

    private static RawSharpMeasuresScalarDefinition DefaultDefiniton() => RawSharpMeasuresScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations, SharpMeasuresScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresScalarProperties.AllProperties) { }
    }
}
