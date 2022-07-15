namespace SharpMeasures.Generators.Quantities.Parsing.IncludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public static class IncludeUnitsParser
{
    public static IAttributeParser<RawUnitListDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitListDefinition DefaultDefinition() => RawUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitListDefinition, UnitListLocations, IncludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, IncludeUnitsProperties.AllProperties) { }
    }
}
