namespace SharpMeasures.Generators.Units.Parsing.FixedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class FixedUnitParser
{
    public static IAttributeParser<RawFixedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawFixedUnitDefinition DefaultDefinition() => RawFixedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawFixedUnitDefinition, FixedUnitParsingData, FixedUnitLocations, FixedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, FixedUnitProperties.AllProperties) { }
    }
}
