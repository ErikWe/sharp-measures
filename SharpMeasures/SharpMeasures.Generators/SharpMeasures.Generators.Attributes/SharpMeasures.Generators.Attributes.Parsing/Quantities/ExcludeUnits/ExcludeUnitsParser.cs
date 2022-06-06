namespace SharpMeasures.Generators.Attributes.Parsing.Quantities;

using SharpMeasures.Generators.Quantities;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<RawExcludeUnits> Parser { get; } = new AttributeParser();

    private static RawExcludeUnits DefaultDefinition() => RawExcludeUnits.Empty;

    private class AttributeParser : AAttributeParser<RawExcludeUnits, ExcludeUnitsLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
