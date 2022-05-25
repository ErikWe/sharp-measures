namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class ExcludeBasesParser
{
    public static IAttributeParser<ExcludeBasesDefinition> Parser { get; } = new AttributeParser();

    private static ExcludeBasesDefinition DefaultDefinition() => ExcludeBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<ExcludeBasesDefinition, ExcludeBasesParsingData, ExcludeBasesLocations, ExcludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeBasesProperties.AllProperties) { }
    }
}
