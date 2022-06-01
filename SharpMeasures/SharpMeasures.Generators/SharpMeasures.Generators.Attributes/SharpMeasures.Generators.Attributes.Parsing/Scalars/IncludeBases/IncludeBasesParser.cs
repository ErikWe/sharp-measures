namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class IncludeBasesParser
{
    public static IAttributeParser<RawIncludeBasesDefinition> Parser { get; } = new AttributeParser();

    private static RawIncludeBasesDefinition DefaultDefinition() => RawIncludeBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawIncludeBasesDefinition, IncludeBasesLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
