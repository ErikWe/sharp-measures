namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;

public static class IncludeUnitsParser
{
    public static IAttributeParser<RawIncludeUnitsDefinition> Parser { get; } = new AttributeParser();

    private static RawIncludeUnitsDefinition DefaultDefinition() => RawIncludeUnitsDefinition.Empty;

    private sealed class AttributeParser : AAttributeParser<RawIncludeUnitsDefinition, IncludeUnitsLocations, IncludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitsProperties.AllProperties) { }
    }
}
