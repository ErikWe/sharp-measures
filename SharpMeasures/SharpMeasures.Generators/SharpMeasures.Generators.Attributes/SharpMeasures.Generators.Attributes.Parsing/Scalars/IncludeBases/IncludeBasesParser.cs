namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class IncludeBasesParser
{
    public static IAttributeParser<IncludeBasesDefinition> Parser { get; } = new AttributeParser();

    private static IncludeBasesDefinition DefaultDefinition() => IncludeBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<IncludeBasesDefinition, IncludeBasesParsingData, IncludeBasesLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
