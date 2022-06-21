namespace SharpMeasures.Generators.Units.Parsing.BiasedUnit;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal static class BiasedUnitParser
{
    public static IAttributeParser<RawBiasedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawBiasedUnitDefinition DefaultDefinition() => RawBiasedUnitDefinition.Empty;

    private class AttributeParser : AUnitParser<RawBiasedUnitDefinition, BiasedUnitParsingData, BiasedUnitLocations, BiasedUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, BiasedUnitProperties.AllProperties) { }
    }
}
