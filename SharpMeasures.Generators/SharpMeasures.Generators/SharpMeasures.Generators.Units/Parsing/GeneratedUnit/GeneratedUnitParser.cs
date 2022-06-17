namespace SharpMeasures.Generators.Units.Parsing.GeneratedUnit;

using SharpMeasures.Generators.Attributes.Parsing;

internal static class GeneratedUnitParser
{
    public static IAttributeParser<RawGeneratedUnitDefinition> Instance { get; } = new AttributeParser();

    private static RawGeneratedUnitDefinition DefaultDefiniton() => RawGeneratedUnitDefinition.Empty;

    private class AttributeParser : AAttributeParser<RawGeneratedUnitDefinition, GeneratedUnitLocations, SharpMeasuresUnitAttribute>
    {
        public AttributeParser() : base(DefaultDefiniton, GeneratedUnitProperties.AllProperties) { }
    }
}
