namespace SharpMeasures.Generators.Units.Parsing.ScaledUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class ScaledUnitParser
{
    public static IAttributeParser<RawScaledUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawScaledUnitDefinition DefaultDefinition() => RawScaledUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawScaledUnitDefinition, ScaledUnitParsingData, ScaledUnitLocations, ScaledUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitProperties.AllProperties) { }
    }
}
