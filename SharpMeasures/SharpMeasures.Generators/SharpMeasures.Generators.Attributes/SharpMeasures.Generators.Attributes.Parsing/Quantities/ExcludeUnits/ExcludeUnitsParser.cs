namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<ExcludeUnitsDefinition> Parser { get; } = new AttributeParser();

    private static ExcludeUnitsDefinition DefaultDefinition() => ExcludeUnitsDefinition.Empty;

    private class AttributeParser : AAttributeParser<ExcludeUnitsDefinition, ExcludeUnitsParsingData, ExcludeUnitsLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
