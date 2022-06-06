namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

using SharpMeasures.Generators.Scalars;

public static class ExcludeBasesParser
{
    public static IAttributeParser<RawExcludeBases> Parser { get; } = new AttributeParser();

    private static RawExcludeBases DefaultDefinition() => RawExcludeBases.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeBases, ExcludeBasesLocations, ExcludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeBasesProperties.AllProperties) { }
    }
}
