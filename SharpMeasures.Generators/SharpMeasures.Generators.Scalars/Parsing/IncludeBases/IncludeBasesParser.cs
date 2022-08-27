namespace SharpMeasures.Generators.Scalars.Parsing.IncludeBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class IncludeBasesParser
{
    public static IAttributeParser<RawIncludeBasesDefinition> Parser { get; } = new AttributeParser();

    private static RawIncludeBasesDefinition DefaultDefinition() => RawIncludeBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawIncludeBasesDefinition, IncludeBasesLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
