namespace SharpMeasures.Generators.Scalars.Parsing.SharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresScalarParser
{
    public static IAttributeParser<SymbolicSharpMeasuresScalarDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresScalarDefinition DefaultDefiniton() => SymbolicSharpMeasuresScalarDefinition.Empty;

    private class AttributeParser : AAttributeParser<SymbolicSharpMeasuresScalarDefinition, SharpMeasuresScalarLocations, SharpMeasuresScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresScalarProperties.AllProperties) { }
    }
}
