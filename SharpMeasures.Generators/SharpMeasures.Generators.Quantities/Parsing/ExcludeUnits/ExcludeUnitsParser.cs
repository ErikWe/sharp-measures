namespace SharpMeasures.Generators.Quantities.Parsing.ExcludeUnits;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities.Parsing.UnitList;

public static class ExcludeUnitsParser
{
    public static IAttributeParser<RawUnitListDefinition> Parser { get; } = new AttributeParser();

    private static RawUnitListDefinition DefaultDefinition() => RawUnitListDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawUnitListDefinition, UnitListLocations, ExcludeUnitsAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ExcludeUnitsProperties.AllProperties) { }
    }
}
