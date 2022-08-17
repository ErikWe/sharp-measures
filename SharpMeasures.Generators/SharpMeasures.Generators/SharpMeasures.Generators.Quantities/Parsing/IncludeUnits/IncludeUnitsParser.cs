namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public static class IncludeUnitsParser
{
    public static IAttributeParser<UnprocessedUnitListDefinition> Parser { get; } = new AttributeParser();

    private static UnprocessedUnitListDefinition DefaultDefinition() => UnprocessedUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<UnprocessedUnitListDefinition, UnitListLocations, IncludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitsProperties.AllProperties) { }
    }
}
