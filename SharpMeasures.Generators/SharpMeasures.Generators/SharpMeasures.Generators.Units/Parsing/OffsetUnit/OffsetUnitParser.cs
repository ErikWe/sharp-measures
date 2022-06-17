namespace SharpMeasures.Generators.Units.Parsing.OffsetUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class OffsetUnitParser
{
    public static IAttributeParser<RawOffsetUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawOffsetUnitDefinition DefaultDefinition() => RawOffsetUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawOffsetUnitDefinition, OffsetUnitParsingData, OffsetUnitLocations, BiasedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, OffsetUnitProperties.AllProperties) { }
    }
}
