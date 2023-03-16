namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class SharpMeasuresVectorGroupMemberParser
{
    public static IAttributeParser<SymbolicSharpMeasuresVectorGroupMemberDefinition> Parser { get; } = new AttributeParser();

    private static SymbolicSharpMeasuresVectorGroupMemberDefinition DefaultDefiniton() => SymbolicSharpMeasuresVectorGroupMemberDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<SymbolicSharpMeasuresVectorGroupMemberDefinition, SharpMeasuresVectorGroupMemberLocations, VectorGroupMemberAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, SharpMeasuresVectorGroupMemberProperties.AllProperties) { }
    }
}
