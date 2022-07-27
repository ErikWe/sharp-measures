namespace SharpMeasures.Generators.Vectors.Parsing.RegisterVectorGroupMember;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class RegisterVectorGroupMemberParser
{
    public static IAttributeParser<RawRegisterVectorGroupMemberDefinition> Parser { get; } = new AttributeParser();

    private static RawRegisterVectorGroupMemberDefinition DefaultDefiniton() => RawRegisterVectorGroupMemberDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawRegisterVectorGroupMemberDefinition, RegisterVectorGroupMemberLocations, SharpMeasuresVectorAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, RegisterVectorGroupMemberProperties.AllProperties) { }
    }
}
