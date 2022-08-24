namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresScalarParser
{
    public static IAttributeParser<RawSpecializedSharpMeasuresScalarDefinition> Parser { get; } = new AttributeParser();

    private static RawSpecializedSharpMeasuresScalarDefinition DefaultDefiniton() => RawSpecializedSharpMeasuresScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations, SpecializedSharpMeasuresScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresScalarProperties.AllProperties) { }
    }
}
