namespace SharpMeasures.Generators.Scalars.Parsing.IncludeUnitBases;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class IncludeUnitBasesParser
{
    public static IAttributeParser<RawIncludeUnitBasesDefinition> Parser { get; } = new AttributeParser();

    private static RawIncludeUnitBasesDefinition DefaultDefinition() => RawIncludeUnitBasesDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawIncludeUnitBasesDefinition, IncludeUnitBasesLocations, IncludeUnitBasesAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitBasesProperties.AllProperties) { }
    }
}
