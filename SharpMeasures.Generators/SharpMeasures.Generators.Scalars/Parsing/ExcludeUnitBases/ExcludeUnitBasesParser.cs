namespace SharpMeasures.Generators.Scalars.Parsing.ExcludeBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ExcludeUnitBasesParser
{
    public static IAttributeParser<RawExcludeUnitBasesDefinition> Parser { get; } = new AttributeParser();

    private static RawExcludeUnitBasesDefinition DefaultDefinition() => RawExcludeUnitBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeUnitBasesDefinition, ExcludeUnitBasesLocations, ExcludeUnitBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitBasesProperties.AllProperties) { }
    }
}
