namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<RawExcludeUnitsDefinition> Parser { get; } = new AttributeParser();

    private static RawExcludeUnitsDefinition DefaultDefinition() => RawExcludeUnitsDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeUnitsDefinition, ExcludeUnitsLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
