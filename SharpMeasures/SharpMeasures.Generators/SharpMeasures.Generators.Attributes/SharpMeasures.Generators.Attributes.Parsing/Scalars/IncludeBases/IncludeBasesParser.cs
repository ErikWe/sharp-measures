namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class IncludeBasesParser
{
    public static IAttributeParser<RawIncludeBases> Parser { get; } = new AttributeParser();

    private static RawIncludeBases DefaultDefinition() => RawIncludeBases.Empty;

    private class AttributeParser : AAttributeParser<RawIncludeBases, IncludeBasesLocations, IncludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeBasesProperties.AllProperties) { }
    }
}
