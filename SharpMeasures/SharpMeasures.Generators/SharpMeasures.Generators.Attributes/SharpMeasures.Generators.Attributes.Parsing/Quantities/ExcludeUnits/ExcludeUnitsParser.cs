namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<RawExcludeUnitsDefinition> Parser { get; } = new AttributeParser();

    private static RawExcludeUnitsDefinition DefaultDefinition() => RawExcludeUnitsDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeUnitsDefinition, ExcludeUnitsLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
