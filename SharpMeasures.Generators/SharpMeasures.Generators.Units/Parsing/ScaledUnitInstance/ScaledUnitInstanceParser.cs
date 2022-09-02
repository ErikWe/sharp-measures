namespace SharpMeasures.Generators.Units.Parsing.ScaledUnitInstance;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class ScaledUnitInstanceParser
{
    public static IAttributeParser<RawScaledUnitInstanceDefinition> Parser { get; } = new AttributeParser();

    private static RawScaledUnitInstanceDefinition DefaultDefinition() => RawScaledUnitInstanceDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawScaledUnitInstanceDefinition, ScaledUnitInstanceLocations, ScaledUnitInstanceAttribute>
    {
        public AttributeParser() : base(DefaultDefinition, ScaledUnitInstanceProperties.AllProperties) { }
    }
}
