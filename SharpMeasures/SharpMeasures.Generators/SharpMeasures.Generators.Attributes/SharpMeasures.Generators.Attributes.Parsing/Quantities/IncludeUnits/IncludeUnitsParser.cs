namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class IncludeUnitsParser
{
    public static IAttributeParser<RawIncludeUnits> Parser { get; } = new AttributeParser();

    private static RawIncludeUnits DefaultDefinition() => RawIncludeUnits.Empty;

    private class AttributeParser : AAttributeParser<RawIncludeUnits, IncludeUnitsLocations, IncludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitsProperties.AllProperties) { }
    }
}
