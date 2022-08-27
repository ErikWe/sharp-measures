namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ExcludeBasesParser
{
    public static IAttributeParser<RawExcludeBasesDefinition> Parser { get; } = new AttributeParser();

    private static RawExcludeBasesDefinition DefaultDefinition() => RawExcludeBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeBasesDefinition, ExcludeBasesLocations, ExcludeBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeBasesProperties.AllProperties) { }
    }
}
