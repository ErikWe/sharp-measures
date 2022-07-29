namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorGroupMemberParser
{
    public static IAttributeParser<RawSharpMeasuresVectorGroupMemberDefinition> Parser { get; } = new AttributeParser();

    private static RawSharpMeasuresVectorGroupMemberDefinition DefaultDefiniton() => RawSharpMeasuresVectorGroupMemberDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations, SharpMeasuresVectorGroupMemberAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorGroupMemberProperties.AllProperties) { }
    }
}
