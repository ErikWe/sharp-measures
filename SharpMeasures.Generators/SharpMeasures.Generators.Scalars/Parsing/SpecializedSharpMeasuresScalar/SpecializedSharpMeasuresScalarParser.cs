namespace SharpMeasures.Generators.Scalars.Parsing.SpecializedSharpMeasuresScalar;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SpecializedSharpMeasuresScalarParser
{
    public static IAttributeParser<SymbolicSpecializedSharpMeasuresScalarDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSpecializedSharpMeasuresScalarDefinition DefaultDefiniton() => SymbolicSpecializedSharpMeasuresScalarDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicSpecializedSharpMeasuresScalarDefinition, SpecializedSharpMeasuresScalarLocations, SpecializedSharpMeasuresScalarAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SpecializedSharpMeasuresScalarProperties.AllProperties) { }
    }
}
