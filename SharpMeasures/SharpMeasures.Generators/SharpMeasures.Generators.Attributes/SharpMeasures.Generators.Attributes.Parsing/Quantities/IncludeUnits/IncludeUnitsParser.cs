namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class IncludeUnitsParser
{
    public static IAttributeParser<IncludeUnitsDefinition> Parser { get; } = new AttributeParser();

    private static IncludeUnitsDefinition DefaultDefinition() => IncludeUnitsDefinition.Empty;

    private class AttributeParser : AAttributeParser<IncludeUnitsDefinition, IncludeUnitsParsingData, IncludeUnitsLocations, IncludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitsProperties.AllProperties) { }
    }
}
